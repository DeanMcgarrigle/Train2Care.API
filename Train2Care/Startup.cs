using Microsoft.Owin.Extensions;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Nancy.Owin;
using Owin;
using Train2Care.Nancy;

namespace Train2Care
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var physicalFileSystem = new PhysicalFileSystem(@".\"); //. = root, Web = your physical directory that contains all other static content, see prev step
            var options = new FileServerOptions
            {
                EnableDefaultFiles = true,
                FileSystem = physicalFileSystem
            };
            options.StaticFileOptions.FileSystem = physicalFileSystem;
            options.StaticFileOptions.ServeUnknownFileTypes = true;
            app.UseFileServer(options);

            app.UseNancy(NancyConfig);
            app.UseStageMarker(PipelineStage.MapHandler);

            //FluentMapper.Initialize(config =>
            //{
            //    config.AddMap(new CustomerDetailsModelMapper());
            //    config.AddMap(new SearchModelMapper());
            //    config.AddMap(new CheckBoxMapper());
            //});
        }

        private static void NancyConfig(NancyOptions nancyOptions)
        {
            nancyOptions.Bootstrapper = new Bootstrapper();
        }
    }
}