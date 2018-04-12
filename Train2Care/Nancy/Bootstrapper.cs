using Nancy;
using Nancy.Conventions;

namespace Train2Care.Nancy
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("build"));
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("app/assets/img"));
        }
    }
}