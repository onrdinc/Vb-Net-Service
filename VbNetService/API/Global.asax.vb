Imports System.Web.Http
Imports Autofac
Imports Autofac.Integration.WebApi
Imports AutoMapper
Imports Business
Imports DataAccess

Public Class WebApiApplication
    Inherits HttpApplication

    Private container As IContainer

    Protected Sub Application_Start()
        container = ConfigureContainer()
        GlobalConfiguration.Configure(AddressOf Register)
    End Sub

    Public Sub Register(ByVal config As HttpConfiguration)
        config.DependencyResolver = New AutofacWebApiDependencyResolver(container)
        config.MapHttpAttributeRoutes()
        config.Routes.MapHttpRoute(
            name:="DefaultApi",
            routeTemplate:="api/{controller}/{id}",
            defaults:=New With {.id = RouteParameter.Optional}
        )
    End Sub

    Private Function ConfigureContainer() As IContainer
        Dim builder As New ContainerBuilder()
        builder.RegisterType(Of Context)().InstancePerLifetimeScope()
        builder.RegisterType(Of CategoryRepository).As(Of ICategoryRepository)()
        builder.RegisterType(Of CategoryBs).As(Of ICategoryBs)()

        Dim mapperConfiguration = New MapperConfiguration(Sub(cfg)
                                                              cfg.AddProfile(Of CategoryProfile)()

                                                          End Sub)

        Dim mapper = mapperConfiguration.CreateMapper()
        builder.RegisterInstance(mapper).As(Of IMapper)()

        builder.RegisterApiControllers(Reflection.Assembly.GetExecutingAssembly())
        Return builder.Build()
    End Function
End Class