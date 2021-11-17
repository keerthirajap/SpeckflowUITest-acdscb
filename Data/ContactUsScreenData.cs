namespace InfinityMatrix.Niraiya.UITests.Data
{
    using InfinityMatrix.Niraiya.UITests.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public static class ContactUsScreenData
    {
        public static List<ContactUsScreenModel> ContactUsScreenModels = new List<ContactUsScreenModel>();

        public static List<ContactUsScreenModel> ContactUs_Screen_Submit_Negative()
        {
            List<ContactUsScreenModel> contactUsScreenModels = new List<ContactUsScreenModel>();

            contactUsScreenModels.Add(new ContactUsScreenModel
            {
                ScenarioId = "ContactUs-1.1",
                TestName = "Test Name - Empty",
                NameValidation = true,
                Name = string.Empty,
                NameEM = "The Name field is required.",
            });

            contactUsScreenModels.Add(new ContactUsScreenModel
            {
                ScenarioId = "ContactUs-1.2",
                TestName = "Less Than 6",
                NameValidation = true,
                Name = "12345",
                NameEM = "The Name must be at least 6 characters long.",
            });

            contactUsScreenModels.Add(new ContactUsScreenModel
            {
                ScenarioId = "ContactUs-1.3",
                TestName = "Test EmailId - Empty",
                EmailIdValidation = true,
                EmailId = string.Empty,
                EmailIdEM = "The Email address field is required.",
            });

            contactUsScreenModels.Add(new ContactUsScreenModel
            {
                ScenarioId = "ContactUs-1.4",
                TestName = "Test EmailId - Invalid",
                EmailIdValidation = true,
                EmailId = Faker.Lorem.Word(),
                EmailIdEM = "Invalid Email address",
            });
            contactUsScreenModels.Add(new ContactUsScreenModel
            {
                ScenarioId = "ContactUs-1.5",
                TestName = "Test EmailId - Invalid Length",
                EmailIdValidation = true,
                EmailId = "1@s.c",
                EmailIdEM = "The Email address must be at least 6 characters long.",
            });

            contactUsScreenModels.Add(new ContactUsScreenModel
            {
                ScenarioId = "ContactUs-1.6",
                TestName = "Test Subject",
                SubjectValidation = true,
                Subject = "",
                SubjectEM = "The Subject field is required.",
            });

            contactUsScreenModels.Add(new ContactUsScreenModel
            {
                ScenarioId = "ContactUs-1.7",
                TestName = "Test Subject _Less than 6",
                SubjectValidation = true,
                Subject = "1234",
                SubjectEM = "The Subject must be at least 6 characters long.",
            });

            contactUsScreenModels.Add(new ContactUsScreenModel
            {
                ScenarioId = "ContactUs-1.8",
                TestName = "Test Subject - Empty",
                MessageValidation = true,
                Message = "",
                MessageEM = "The Message field is required.",
            });

            contactUsScreenModels.Add(new ContactUsScreenModel
            {
                ScenarioId = "ContactUs-1.9",
                TestName = "Test Subject - Less 6",
                MessageValidation = true,
                Message = "1234",
                MessageEM = "The Message must be at least 6 characters long.",
            });

            contactUsScreenModels.Add(new ContactUsScreenModel
            {
                ScenarioId = "ContactUs-1.10",
                TestName = "Test Subject - More Than 1000",
                MessageValidation = true,
                Message = string.Join("", Faker.Lorem.Sentences(20)),
                MessageEM = "The field Description must be a string or array type with a maximum length of '1000'.",
            });

            contactUsScreenModels.Add(new ContactUsScreenModel
            {
                ScenarioId = "ContactUs-1.11",
                TestName = "Test AgreePrivacyPolicy - Empty",
                AgreePrivacyPolicyValidation = true,
                AgreePrivacyPolicy = false,
                AgreePrivacyPolicyEM = "Please agree with our Privacy And Security policy.",
            });

            contactUsScreenModels.Add(new ContactUsScreenModel
            {
                ScenarioId = "ContactUs-1.12",
                TestName = "Validation Error for Name",
                NameValidation = true,
                Name = string.Empty,
                NameEM = "The Name field is required.",
                EmailId = Faker.User.Email(),
                Subject = string.Join("", Faker.Lorem.Sentences(1)),
                Message = string.Join("", Faker.Lorem.Sentences(5)),
                AgreePrivacyPolicy = true,
            });

            contactUsScreenModels.Add(new ContactUsScreenModel
            {
                ScenarioId = "ContactUs-1.13",
                TestName = "Validation Error for Email",
                Name = Faker.Name.FirstName() + "" + Faker.Name.LastName(),
                EmailIdValidation = true,
                EmailId = string.Empty,
                EmailIdEM = "The Email address field is required.",
                Subject = string.Join("", Faker.Lorem.Sentences(1)),
                Message = string.Join("", Faker.Lorem.Sentences(5)),
                AgreePrivacyPolicy = true,
            });

            contactUsScreenModels.Add(new ContactUsScreenModel
            {
                ScenarioId = "ContactUs-1.14",
                TestName = "Validation Error for Subject",
                Name = Faker.Name.FirstName() + "" + Faker.Name.LastName(),
                EmailId = Faker.User.Email(),
                SubjectValidation = true,
                Subject = string.Empty,
                SubjectEM = "The Subject field is required.",
                Message = string.Join("", Faker.Lorem.Sentences(1)),
                AgreePrivacyPolicy = true,
            });

            contactUsScreenModels.Add(new ContactUsScreenModel
            {
                ScenarioId = "ContactUs-1.15",
                TestName = "Validation Error for Message 1",
                Name = Faker.Name.FirstName() + "" + Faker.Name.LastName(),
                EmailId = Faker.User.Email(),
                Subject = string.Join("", Faker.Lorem.Sentences(5)),
                MessageValidation = true,
                Message = string.Join("", Faker.Lorem.Sentences(20)),
                MessageEM = "The field Description must be a string or array type with a maximum length of '1000'.",
                AgreePrivacyPolicy = true,
            });

            contactUsScreenModels.Add(new ContactUsScreenModel
            {
                ScenarioId = "ContactUs-1.16",
                TestName = "Validation Error for Message 2",
                Name = Faker.Name.FirstName() + "" + Faker.Name.LastName(),
                EmailId = Faker.User.Email(),
                Subject = string.Join("", Faker.Lorem.Sentences(5)),
                MessageValidation = true,
                Message = string.Empty,
                MessageEM = "The Message field is required.",
                AgreePrivacyPolicy = true,
            });

            contactUsScreenModels.Add(new ContactUsScreenModel
            {
                ScenarioId = "ContactUs-1.17",
                TestName = "Validation Error for AgreePrivacyPolicy",
                Name = "1234567",
                EmailId = Faker.User.Email(),
                Subject = string.Join("", Faker.Lorem.Sentences(1)),
                Message = string.Join("", Faker.Lorem.Sentences(5)),
                AgreePrivacyPolicyValidation = true,
                AgreePrivacyPolicy = false,
                AgreePrivacyPolicyEM = "Please agree with our Privacy And Security policy.",
            });

            return contactUsScreenModels;
        }

        public static List<ContactUsScreenModel> ContactUs_Screen_Submit_Positive()
        {
            List<ContactUsScreenModel> contactUsScreenModels = new List<ContactUsScreenModel>();

            contactUsScreenModels.Add(new ContactUsScreenModel
            {
                ScenarioId = "ContactUs-2.10",
                TestName = "All Valid",
                NameValidation = true,
                Name = Faker.Name.FirstName() + "" + Faker.Name.LastName(),

                EmailIdValidation = true,
                EmailId = "joesphked2020dec@gmail.com",

                SubjectValidation = true,
                Subject = "Auto-Test " + string.Join("", Faker.Lorem.Sentences(1)),

                MessageValidation = true,
                Message = string.Join("", Faker.Lorem.Sentences(5)),

                AgreePrivacyPolicyValidation = true,
                AgreePrivacyPolicy = true,
            });

            contactUsScreenModels.Add(new ContactUsScreenModel
            {
                ScenarioId = "ContactUs-2.11",
                TestName = "All Valid",
                NameValidation = true,
                Name = Faker.Name.FirstName() + Faker.Name.LastName(),

                EmailIdValidation = true,
                EmailId = "joesphked2020dec@gmail.com",

                SubjectValidation = true,
                Subject = "Auto-Test " + string.Join("", Faker.Lorem.Sentences(1)),

                MessageValidation = true,
                Message = string.Join("", Faker.Lorem.Sentences(5)),

                AgreePrivacyPolicyValidation = true,
                AgreePrivacyPolicy = true,
            }); ;

            contactUsScreenModels.Add(new ContactUsScreenModel
            {
                ScenarioId = "ContactUs-2.12",
                TestName = "All Valid",
                NameValidation = true,
                Name = Faker.Name.FirstName() + Faker.Name.LastName(),

                EmailIdValidation = true,
                EmailId = "joesphked2020dec@gmail.com",

                SubjectValidation = true,
                Subject = "Auto-Test " + string.Join("", Faker.Lorem.Sentences(1)),
                MessageValidation = true,
                Message = string.Join("", Faker.Lorem.Sentences(5)),

                AgreePrivacyPolicyValidation = true,
                AgreePrivacyPolicy = true,
            });

            contactUsScreenModels.Add(new ContactUsScreenModel
            {
                ScenarioId = "ContactUs-2.13",
                TestName = "All Valid",
                NameValidation = true,
                Name = Faker.Name.FirstName() + Faker.Name.LastName(),

                EmailIdValidation = true,
                EmailId = "joesphked2020dec@gmail.com",

                SubjectValidation = true,
                Subject = "Auto-Test " + string.Join("", Faker.Lorem.Sentences(1)),

                MessageValidation = true,
                Message = string.Join("", Faker.Lorem.Sentences(5)),

                AgreePrivacyPolicyValidation = true,
                AgreePrivacyPolicy = true,
            });

            return contactUsScreenModels;
        }
    }
}