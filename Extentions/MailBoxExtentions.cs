namespace InfinityMatrix.Niraiya.UITests.Extentions
{
    using MailKit;
    using MailKit.Net.Imap;
    using MailKit.Search;
    using Microsoft.Extensions.Configuration;
    using MimeKit;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Firefox;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading;

    public static class MailBoxExtentions
    {
        public static void DeleteAllEmails(string emailId, string password)
        {
            using (var client = new ImapClient())
            {
                client.Connect("imap.gmail.com", 993, true);

                client.Authenticate(emailId, password);

                var inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadWrite);

                // fetch some useful metadata about each message in the folder...
                var items = inbox.Fetch(0, -1, MessageSummaryItems.UniqueId | MessageSummaryItems.Size | MessageSummaryItems.Flags);

                // iterate over all of the messages and fetch them by UID
                foreach (var item in items)
                {
                    var inbox1 = client.Inbox;
                    inbox1.Open(FolderAccess.ReadWrite);

                    var message = inbox1.GetMessage(item.UniqueId);

                    var trash = client.GetFolder(SpecialFolder.Trash);

                    var moved = client.Inbox.MoveTo(item.UniqueId, trash);

                    if (moved.HasValue)
                    {
                        trash.Open(FolderAccess.ReadWrite);
                        trash.AddFlags(moved.Value, MessageFlags.Deleted, true);
                        Thread.Sleep(1000);
                        trash.Expunge(new[] { moved.Value });
                        Thread.Sleep(1000);
                    }
                }

                //for (int i = 0; i < inbox.Count; i++)
                //{
                //    //client.Inbox.AddFlags(i, MessageFlags.Deleted, true);

                //    var vvv = inbox[i];

                //    var trash = client.GetFolder(SpecialFolder.Trash);
                //    var moved = client.Inbox.MoveTo(uid, trash);

                //    if (moved.HasValue)
                //    {
                //        trash.Open(FolderAccess.ReadWrite);
                //        trash.AddFlags(moved.Value, MessageFlags.Deleted, true);
                //        trash.Expunge(new[] { moved.Value });
                //    }
                //}

                //Thread.Sleep(2000);

                //var trash = client.GetFolder(SpecialFolder.Trash);

                //trash.Open(FolderAccess.ReadWrite);

                //for (int i = 0; i < trash.Count; i++)
                //{
                //    trash.AddFlags(i, MessageFlags.Deleted, true);
                //}

                client.Disconnect(true);
            }
        }

        public static MimeMessage SearchByEmailSubject(string emailId, string password, string subject)
        {
            using (var client = new ImapClient())
            {
                client.Connect("imap.gmail.com", 993, true);

                client.Authenticate(emailId, password);

                var inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadWrite);

                var uid = inbox.Search(MailKit.Search.SearchOptions.All, SearchQuery.SubjectContains(subject)).UniqueIds.FirstOrDefault();

                var message = inbox.GetMessage(uid);

                return message;
            }
        }

        public static MimeMessage SearchLatestEmailBySubject(string emailId, string password, string subject)
        {
            using (var client = new ImapClient())
            {
                client.Connect("imap.gmail.com", 993, true);

                client.Authenticate(emailId, password);

                var inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadWrite);

                var uid = inbox.Search(MailKit.Search.SearchOptions.All, SearchQuery.SubjectContains(subject)).UniqueIds.OrderByDescending(x => x.Id).FirstOrDefault();

                var message = inbox.GetMessage(uid);

                return message;
            }
        }
    }
}