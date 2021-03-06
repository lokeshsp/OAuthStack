﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using OAuthStack.Common.Security;
using OAuthStack.Common.ServiceModel;
using OAuthStack.Common.Services;
using OAuthStack.FakeServices;
using OAuthStack.UserDataServer.ServiceModel;
using ServiceStack.Logging;
using ServiceStack.WebHost.Endpoints;

namespace OAuthStack.UserDataServer.Infrastructure {
    public class AppHost : AppHostBase {
        public AppHost() //Tell ServiceStack the name and where to find your web services
            : base("OAuth2 Demo Resource Server", typeof(UserService).Assembly) { }

        public override void Configure(Funq.Container container) {
            //Set JSON web services to return idiomatic JSON camelCase properties
            ServiceStack.Text.JsConfig.EmitCamelCaseNames = true;

            //Configure User Defined REST Paths
            Routes
              .Add<Users>("/users")
              .Add<Users>("/users/{Username}");

            //Register all your dependencies		
            ;


            container.Register("authServer", c =>
                CryptoKeyPair.LoadCertificate(HostingEnvironment.MapPath("~/bin/Certificates/auth-server.pfx"), "p@ssw0rd"));
            container.Register("dataServer", c =>
                CryptoKeyPair.LoadCertificate(HostingEnvironment.MapPath("~/bin/Certificates/data-server.pfx"), "p@ssw0rd"));
            container.RegisterAutoWiredAs<FakeUserStore, IUserStore>();
        }

    }
}
