# ASP.NET MVC Monolith Structure

This project contains a sample application that shows mechanism used to create "Package by Feature" architecture using ASP.NET MVC 5 technology stack.

The structure is based on idea that each major business feature forms a seperate `app`.

## Distributing web app building blocks

**Controllers**

Each app resides in it's own MVC area, and requires several steps in registration (IoC, MVC controller namespaces and routes):

    container.RegisterControllers(app.GetType().Assembly);
    ControllerBuilder.Current.DefaultNamespaces.Add(app.GetType().Namespace);
    DefaultRoute.CreateAppRoute(app.GetType());
            
**Static files**

Static files from each app are copied to final web app during build using `msbuild` custom target. By convention, all static files should reside in `Content` subfolder:

    <ItemGroup>
        <ModuleFiles Include="$(ProjectDir)\..\*\Content\**\*.*" Exclude="$(ProjectDir)\..\$(ProjectName)\Content\**\*.*">            
        </ModuleFiles>
    </ItemGroup>

    <Target Name="CopyModuleFiles">
        <Copy SourceFiles="@(ModuleFiles)" DestinationFiles="@(ModuleFiles->'$(ProjectDir)Content\%(RecursiveDir)..\%(Filename)%(Extension)')" />
    </Target>

    <Target Name="BeforeBuild" DependsOnTargets="CopyModuleFiles">
    
    
**Razor files**

To load razor views from seperate class libraries the `RazorGenerator` is used. Custom `VirtualPathProvider` (enabled only in debug) is used to make it possible to edit the files without recompiling entire application.

asdstring
Version:0.9 StartHTML:0000000149 EndHTML:0000001431 StartFragment:0000000185 EndFragment:0000001395 SourceURL:http://localhost:52127/Home/Home
**Test Strong**

<sup>Test Sup</sup>

