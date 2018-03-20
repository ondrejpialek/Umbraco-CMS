﻿using System;
using System.ComponentModel;
using System.Configuration;
using System.Linq;

namespace Umbraco.Core.Configuration.UmbracoSettings
{

    public class UmbracoSettingsSection : ConfigurationSection, IUmbracoSettingsSection
    {
        [ConfigurationProperty("backOffice")]
        internal BackOfficeElement BackOffice
        {
            get { return (BackOfficeElement)this["backOffice"]; }
        }

        [ConfigurationProperty("content")]
        internal ContentElement Content
        {
            get { return (ContentElement)this["content"]; }
        }

        [ConfigurationProperty("security")]
        internal SecurityElement Security
        {
            get { return (SecurityElement)this["security"]; }
        }

        [ConfigurationProperty("requestHandler")]
        internal RequestHandlerElement RequestHandler
        {
            get { return (RequestHandlerElement)this["requestHandler"]; }
        }

        [ConfigurationProperty("templates")]
        internal TemplatesElement Templates
        {
            get { return (TemplatesElement)this["templates"]; }
        }

        [ConfigurationProperty("developer")]
        internal DeveloperElement Developer
        {
            get { return (DeveloperElement)this["developer"]; }
        }


        [ConfigurationProperty("logging")]
        internal LoggingElement Logging
        {
            get { return (LoggingElement)this["logging"]; }
        }

        [ConfigurationProperty("scheduledTasks")]
        internal ScheduledTasksElement ScheduledTasks
        {
            get { return (ScheduledTasksElement)this["scheduledTasks"]; }
        }

        [ConfigurationProperty("distributedCall")]
        internal DistributedCallElement DistributedCall
        {
            get { return (DistributedCallElement)this["distributedCall"]; }
        }

        private RepositoriesElement _defaultRepositories;

        [ConfigurationProperty("repositories")]
        internal RepositoriesElement PackageRepositories
        {
            get
            {

                if (_defaultRepositories != null)
                {
                    return _defaultRepositories;
                }

                //here we need to check if this element is defined, if it is not then we'll setup the defaults
                var prop = Properties["repositories"];
                var repos = this[prop] as ConfigurationElement;
                if (repos != null && repos.ElementInformation.IsPresent == false)
                {
                    var collection = new RepositoriesCollection
                        {
                            new RepositoryElement() {Name = "Umbraco package Repository", Id = new Guid("65194810-1f85-11dd-bd0b-0800200c9a66")}
                        };


                    _defaultRepositories = new RepositoriesElement()
                        {
                            Repositories = collection
                        };

                    return _defaultRepositories;
                }

                //now we need to ensure there is *always* our umbraco repo! its hard coded in the codebase!
                var reposElement = (RepositoriesElement)base["repositories"];
                if (reposElement.Repositories.All(x => x.Id != new Guid("65194810-1f85-11dd-bd0b-0800200c9a66")))
                {
                    reposElement.Repositories.Add(new RepositoryElement() { Name = "Umbraco package Repository", Id = new Guid("65194810-1f85-11dd-bd0b-0800200c9a66") });
                }

                return reposElement;
            }
        }

        [ConfigurationProperty("providers")]
        internal ProvidersElement Providers
        {
            get { return (ProvidersElement)this["providers"]; }
        }

        [ConfigurationProperty("web.routing")]
        internal WebRoutingElement WebRouting
        {
            get { return (WebRoutingElement)this["web.routing"]; }
        }

        IContentSection IUmbracoSettingsSection.Content
        {
            get { return Content; }
        }

        ISecuritySection IUmbracoSettingsSection.Security
        {
            get { return Security; }
        }

        IRequestHandlerSection IUmbracoSettingsSection.RequestHandler
        {
            get { return RequestHandler; }
        }

        ITemplatesSection IUmbracoSettingsSection.Templates
        {
            get { return Templates; }
        }

        IBackOfficeSection IUmbracoSettingsSection.BackOffice
        {
            get { return BackOffice; }
        }

        IDeveloperSection IUmbracoSettingsSection.Developer
        {
            get { return Developer; }
        }

        ILoggingSection IUmbracoSettingsSection.Logging
        {
            get { return Logging; }
        }

        IScheduledTasksSection IUmbracoSettingsSection.ScheduledTasks
        {
            get { return ScheduledTasks; }
        }

        IDistributedCallSection IUmbracoSettingsSection.DistributedCall
        {
            get { return DistributedCall; }
        }

        IRepositoriesSection IUmbracoSettingsSection.PackageRepositories
        {
            get { return PackageRepositories; }
        }

        IProvidersSection IUmbracoSettingsSection.Providers
        {
            get { return Providers; }
        }

        IWebRoutingSection IUmbracoSettingsSection.WebRouting
        {
            get { return WebRouting; }
        }

    }
}
