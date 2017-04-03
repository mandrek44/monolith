# ASP.NET MVC Structured Monolith 

This project contains a sample application that shows mechanism used to create "Package by Feature" architecture using ASP.NET MVC 5 technology stack.

The structure is based on idea that each major business feature forms a seperate app in form of class library.

## Putting web app building blocks in Class Libraries

### Controllers

To use the controllers from different assembly, register controller namespace in `ControllerBuilder.Current.DefaultNamespaces`. 
  
It's best to keep each assembly controllers in seperate areas. To achieve this, register route for each area with namespace constraint: 
  
    var route = RouteTable.Routes.MapRoute(
        name: appName,
        url: appName + "/{controller}/{action}/{id}",
        defaults: new {controller = "Root", action = "Index", id = UrlParameter.Optional },
        namespaces: new[] {appType.Namespace})

Remebmer to specify the Area for created route with `DataTokens` property:
                    
    route.DataTokens["area"] = appName;

Additionaly disable the namespace fallback to force the routes are being evaluated accordingly to constraints:

    route.DataTokens["UseNamespaceFallback"] = false;


### Razor files

`RazorGenerator` can be used to load razor views from seperate class libraries the is used. Use `RazorGenerator.MsBuild` package to rebuild the razor views with each build. 

The compiled views become part of the assembly and therefore cannot be changed during runtime. This means that to fix even a single typo, entire assembly needs to be recompiled which will cause the IIS app-pool to reload.

To make the development cycle faster, it's possible to implement custom `VirtualPathProvider` that will locate the physical files (in their project folders) instrad of emvedded precompiled razor views.

Additionaly the `RazorGenerator.PrecompiledRazorEngine` must be made aware of this additional Path provider so that it will be able to determine if the physical file is newer in order to serve it.

See `ABC.Infrastructure.Web.Defaults.VirtualPath.ABCVirtualPathProvider`.
            
### Static files

Static files from each app are copied to final web app during build using `msbuild` custom target. By convention, all static files should reside in `Content` subfolder:

    <ItemGroup>
        <ModuleFiles Include="$(ProjectDir)\..\*\Content\**\*.*" Exclude="$(ProjectDir)\..\$(ProjectName)\Content\**\*.*">            
        </ModuleFiles>
    </ItemGroup>

    <Target Name="CopyModuleFiles">
        <Copy SourceFiles="@(ModuleFiles)" DestinationFiles="@(ModuleFiles->'$(ProjectDir)Content\%(RecursiveDir)..\%(Filename)%(Extension)')" />
    </Target>

    <Target Name="BeforeBuild" DependsOnTargets="CopyModuleFiles">
    
File `ABC.Infrastructure.Web\Custom.targets` contains full definition of this target. Besides file copying, it also adds the Content files to packaging targets.


