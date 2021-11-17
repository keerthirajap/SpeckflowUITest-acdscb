namespace InfinityMatrix.Niraiya.UITests.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ContactUsScreenModel
    {
        public string ScenarioId { get; set; }

        public string TestName { get; set; }

        public bool NameValidation { get; set; }

        public string Name { get; set; }

        public string NameEM { get; set; }

        public bool EmailIdValidation { get; set; }

        public string EmailId { get; set; }

        public string EmailIdEM { get; set; }

        public bool SubjectValidation { get; set; }

        public string Subject { get; set; }

        public string SubjectEM { get; set; }

        public bool MessageValidation { get; set; }

        public string Message { get; set; }

        public string MessageEM { get; set; }

        public bool AgreePrivacyPolicyValidation { get; set; }

        public bool AgreePrivacyPolicy { get; set; }

        public string AgreePrivacyPolicyEM { get; set; }
    }
}