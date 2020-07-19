﻿using Autofac;
using Strive.Library.Configuration.Emails;
using Module = Autofac.Module;

namespace Strive.Library.Emails
{
    public class EmailModule : Module
    {
        private readonly IEmailSender _emailSender;
        private readonly EmailsSettings _emailsSettings;

        public EmailModule(IEmailSender emailSender, EmailsSettings emailsSettings)
        {
            _emailSender = emailSender;
            _emailsSettings = emailsSettings;
        }

        public EmailModule(EmailsSettings emailsSettings)
        {
            _emailsSettings = emailsSettings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            if (_emailSender != null)
            {
                builder.RegisterInstance(_emailSender);
            }
            else
            {
                builder.RegisterType<EmailSender>()
                    .As<IEmailSender>()
                    .InstancePerLifetimeScope();
            }

            builder.RegisterInstance(_emailsSettings);
        }
    }
}