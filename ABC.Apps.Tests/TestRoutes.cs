using System.Linq;
using System.Reflection;
using ABC.Infrastructure.Contracts;
using ABC.Support;

namespace ABC.Apps.Tests
{
    public class TestRoutes
    {
        public void AppRoutesAreUnique()
        {
            var apps = new[] {typeof(SupportApp)};

            var allRoutes = apps.Select(app => app.GetCustomAttribute<AppNameAttribute>()?.Route ?? AppNameAttribute.GetDefaultRoute(app)).ToArray();

            var areUnique = allRoutes.Length == allRoutes.Distinct().Count();
            // TODO: Assert
        }
    }
}
