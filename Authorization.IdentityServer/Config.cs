// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;

namespace Authorization.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Phone(),
                new IdentityResources.Address(),
                new IdentityResources.Email
                {
                   DisplayName="修改过的DisplayName",
                   Name="身份资源的唯一名称",
                   Description="描述",
                   Required=true,
                }
            };

        public static IEnumerable<ApiResource> ApiResources =>
           new ApiResource[]
           {
                new ApiResource()
                {
                    Name ="Api1",
                    DisplayName ="测试Api",
                    Scopes = new List<string>{ "ClientScope" }
                }
           };


        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope()
                {
                    Name="ClientScope"
                }

            };

        public static List<TestUser> Users => new List<TestUser>
        {
            new TestUser
            {
                SubjectId="1",
                Username="name",
                Password="password",
                Claims =
                {
                    new System.Security.Claims.Claim(JwtClaimTypes.Name,"jwtName"),
                    new System.Security.Claims.Claim(JwtClaimTypes.GivenName,"jwtGivenName"),
                    new System.Security.Claims.Claim(JwtClaimTypes.FamilyName,"jwtFamilyName"),
                    new System.Security.Claims.Claim(JwtClaimTypes.Email,"jwtEmail"),
                    new System.Security.Claims.Claim(JwtClaimTypes.EmailVerified,"jwtEmailVerified"),
                    new System.Security.Claims.Claim(JwtClaimTypes.WebSite,"jwtWebSite"),
                    new System.Security.Claims.Claim(JwtClaimTypes.Address,"jwtAddress"),
                }
            }
        };

        public static IEnumerable<Client> Clients => new Client[]
            {
                new Client()
                {
                    ClientId ="ClientPattern",
                    AllowedGrantTypes=GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("ClientPatternSecret".Sha256())
                    },
                    AllowedScopes={ "ClientScope" }
                },
                new Client()
                {
                    ClientId ="PassPattern",
                    AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("PassPatternSecret".Sha256())
                    },
                    AllowedScopes={ 
                        "ClientScope",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                    }
                },

            };
    }
}