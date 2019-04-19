using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Wizard.Models;

namespace Wizard.Controllers
{
    [Route("api")]
    public class RequestController : Controller
    {

        public RequestController()
        {
            AddTestItemsToRequests();
        }

        public List<Request> TestRequests = new List<Request>();

        //Get Requests
        [HttpGet("GetRequests/{Requester}")]
        public async Task<IActionResult> GetRequests(string Requester)
        {

        
            ReturnRequests returnRequests = new ReturnRequests();

            returnRequests.OwnRequests = (from tr in TestRequests
                                          where tr.Requester == Requester
                                          select tr).ToArray();

            // You need to change "Manager" keyword here and select Requester roles and insted == write 
            // if (UsersWithManagerRole.includes(Requester))
            // do it by your database scheme
            if (Requester == "Supervisor")
            {
                // Here you need to select all request which approved creator and Requester is manager of creators 
                // I do not know your database scheme, so you need to do it with your scheme
                returnRequests.RequestsToApprove = (from tr in TestRequests
                                                    where tr.RequestStatus.Contains("Creator Approved")
                                                    select tr).ToArray();
            }
            if (Requester == "Director")
            {
                // Here you need to select all request which approved creator and Requester is manager of creators 
                // I do not know your database scheme, so you need to do it with your scheme
                returnRequests.RequestsToApprove = (from tr in TestRequests
                                                    where tr.RequestStatus.Contains("Supervisor Approved")
                                                    select tr).ToArray();
            }



            return new OkObjectResult(returnRequests);
        }


        // Add Request
        // Add request also in database
        [HttpPost("AddRequest")]
        public async Task<IActionResult> AddRequest([FromBody] Request TestRequest)
        {
            if (ModelState.IsValid)
            {
                TestRequests.Add(TestRequest);
                return new OkObjectResult("OK");
            }
            return new BadRequestObjectResult("ModelState is not valid");
        }

        //////// Update request also in database
        [HttpPost("UploadRequestNodeFile")]
        public async Task<IActionResult> UploadRequestNodeFile()
        {
            return new OkObjectResult("");
        }

        //////// Remove request also in database
        [HttpPost("RemoveRequest/{RequestNo}")]
        public async Task<IActionResult> RemoveRequest(int RequestNo)
        {
            var remove = TestRequests.Find(t => t.RequestNo == RequestNo);
            TestRequests.Remove(remove);

            return new OkObjectResult(remove);
        }

        // Submit Request
        //////// Submit request also in database
        [HttpPost("SubmitRequest/{RequestNo}")]
        public async Task<IActionResult> SubmitRequest(int RequestNo)
        {
            var T = TestRequests;
            var TT = T;
            var agree = TestRequests.Find(t => t.RequestNo == RequestNo);
            switch (agree.RequestStatus)
            {
                case ("Request Generated"):
                    agree.RequestStatus = "Creator Approved";
                    break;
                case ("Creator Approved"):
                    agree.RequestStatus = "Supervisor Approved";
                    break;
                case ("Supervisor Approved"):
                    agree.RequestStatus = "Director Approved";
                    break;
            }

            SendEmail(RequestNo, agree.RequestStatus);

            return new OkObjectResult(agree);
        }

        // Disagree Request
        // Disagree request also in database
        [HttpPost("DisagreeRequest/{RequestNo}")]
        public async Task<IActionResult> DisagreeRequest(int RequestNo)
        {
            var disagree = TestRequests.Find(t => t.RequestNo == RequestNo);
            disagree.RequestStatus = "Request Generated";

            return new OkObjectResult(disagree);
        }
        


        // Send email to supervisors for submitting or to user after director submits
        public async void SendEmail(int requestNo, string sendToStatus)
        {

            string mailToSend = "";

            switch (sendToStatus)
            {
                case ("Creator Approved"):
                    // Mail Do not Exist
                    mailToSend = "testemaillforsupervisorinwizardapplication@gmail.com";
                    break;
                case ("Supervisor Approved"):
                    // Mail Do not Exist

                    mailToSend = "testemaillfordirectorinwizardapplication@gmail.com";
                    break;
                case ("Director Approved"):
                    // Mail Do not Exist
                    mailToSend = "testemaillforuserinwizardapplication@gmail.com";
                    break;
            }


            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("testapplicationtosendwizard@gmail.com");
            mail.To.Add(mailToSend);
            mail.Subject = sendToStatus == "Director Approved" ? "Request Approved" : "Request needs to approve";
            mail.Body = sendToStatus == "Director Approved" ? "Your Request: " + requestNo + "Approved. <a href=\"https://Yourdomain.com/view/" + requestNo + "\">click here</a> to see request" : "Please Approve request. <a href=\"https://Yourdomain.com/view/" + requestNo + "\">click here</a> to move on page";

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("testapplicationtosendwizard@gmail.com", "Password1!@");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }




        // You wil not use it when add database integration
        public void AddTestItemsToRequests()
        {
            TestRequests.Add(new Request
            {
                RequestNo = 1,
                Requester = "Davit Gogshelidze",
                RequestOffice = "ITS",
                RequestOffice2 = "Main Office",
                RequestDate = DateTime.UtcNow,
                RequestStatus = "Request Generated",
                RequestType = "Contract Service",
                RequiredDeliveryDate = DateTime.UtcNow,
                TechnicalContact = "Tech Content",
                Phone = "+49 123 45678901",
                TechnicalApprovalRequired = true,
                Description = "This is Description For Request Item",
                RenewalContract = true,
                Supplier = "Microsoft",
                LineItems = new LineItem[2]
                {
                    new LineItem
                        {
                            LineId = 1,
                            RequestId = 1,
                            BudgetObject = 10000.00,
                            Description = "Description",
                            Quantity = 3,
                            Unit = "KG",
                            UnitPrice = 100.00,
                            Amount = 100.00,
                            AccLines = new AccLine[3]
                            {
                                new AccLine
                                {
                                    ItemId = 1,
                                    LineId = 1,
                                    SubCenterNo = 4444,
                                    Amount = 3
                                },
                                new AccLine
                                {
                                    ItemId = 2,
                                    LineId = 1,
                                    SubCenterNo = 11111,
                                    Amount = 2
                                },
                                new AccLine
                                {
                                    ItemId = 3,
                                    LineId = 1,
                                    SubCenterNo = 22222,
                                    Amount = 4
                                }
                            }
                        },
                    new LineItem
                                            {
                            LineId = 1,
                            BudgetObject = 856,
                            Description = "Description 2",
                            Quantity = 1,
                            RequestId = 1,
                            Unit = "L",
                            UnitPrice = 211.54,
                            Amount = 100.00,
                            AccLines = new AccLine[3]
                            {
                                new AccLine
                                {
                                    ItemId = 1,
                                    LineId = 1,
                                    SubCenterNo = 4444,
                                    Amount = 3
                                },
                                new AccLine
                                {
                                    ItemId = 2,
                                    LineId = 1,
                                    SubCenterNo = 11111,
                                    Amount = 2
                                },
                                new AccLine
                                {
                                    ItemId = 3,
                                    LineId = 1,
                                    SubCenterNo = 22222,
                                    Amount = 4
                                }
                            }
                        },
                },
                Notes = new Note[1]
                {
                    new Note
                    {
                        NoteId = 1,
                        RequestId = 1,
                        NoteType = "Requester",
                        Content = "This is note of Requester",
                        NoteFiles = new string[] { "814f86f1-3090-495e-bbd6-afbfda293540", "74a16d29-1bf4-480a-8e61-e2ee53fe1070"}
                    }
                }
            });






            TestRequests.Add(new Request
            {
                RequestNo = 2,
                Requester = "Davit Gogshelidze",
                RequestOffice = "ITS",
                RequestOffice2 = "Secondary Office",
                RequestDate = DateTime.UtcNow,
                RequestStatus = "Creator Approved",
                RequestType = "Request Type 2",
                RequiredDeliveryDate = DateTime.UtcNow,
                TechnicalContact = "Books",
                Phone = "+49 123 10987654",
                TechnicalApprovalRequired = false,
                Description = "22 This is Description For Request Item 2",
                RenewalContract = false,
                Supplier = "Microsoft",
                LineItems = new LineItem[2]
            {   new LineItem
                            {
                                LineId = 1,
                                RequestId = 2,
                                BudgetObject = 1500.00,
                                Description = "this is Description",
                                Quantity = 2,
                                Unit = "KG",
                                UnitPrice = 100.00,
                                Amount = 100.00,
                                AccLines = new AccLine[3]
                                {
                                    new AccLine
                                    {
                                        ItemId = 1,
                                        LineId = 1,
                                        SubCenterNo = 4444,
                                        Amount = 3
                                    },
                                    new AccLine
                                    {
                                        ItemId = 2,
                                        LineId = 1,
                                        SubCenterNo = 11111,
                                        Amount = 2
                                    },
                                    new AccLine
                                    {
                                        ItemId = 3,
                                        LineId = 1,
                                        SubCenterNo = 22222,
                                        Amount = 4
                                    }
                                }
                            },
                        new LineItem
                                                {
                                LineId = 1,
                                BudgetObject = 856,
                                Description = "Description 2",
                                Quantity = 1,
                                RequestId = 1,
                                Unit = "L",
                                UnitPrice = 211.54
                            },
            },
                Notes = new Note[1]
            {
                        new Note
                        {
                            NoteId = 1,
                            RequestId = 1,
                            NoteType = "Requester",
                            Content = "This is note of Requester",
                            NoteFiles = new string[] { "814f86f1-3090-495e-bbd6-afbfda293540", "74a16d29-1bf4-480a-8e61-e2ee53fe1070"}
                        }
            }
            });
            TestRequests.Add(new Request
            {
                RequestNo = 3,
                Requester = "Davit Gogshelidze",
                RequestOffice = "ITS",
                RequestOffice2 = "Main Office",
                RequestDate = DateTime.UtcNow,
                RequestStatus = "Request Generated",
                RequestType = "Contract Service",
                RequiredDeliveryDate = DateTime.UtcNow,
                TechnicalContact = "Tech Content",
                Phone = "+49 123 45678901",
                TechnicalApprovalRequired = true,
                Description = "This is Description For Request Item",
                RenewalContract = true,
                Supplier = "Microsoft",
                LineItems = new LineItem[2]
                    {   new LineItem
                            {
                                LineId = 1,
                                RequestId = 1,
                                BudgetObject = 10000.00,
                                Description = "Description",
                                Quantity = 3,
                                Unit = "KG",
                                UnitPrice = 100.00,
                                AccLines = new AccLine[3]
                                {
                                    new AccLine
                                    {
                                        ItemId = 1,
                                        LineId = 1,
                                        SubCenterNo = 4444,
                                        Amount = 3
                                    },
                                    new AccLine
                                    {
                                        ItemId = 2,
                                        LineId = 1,
                                        SubCenterNo = 11111,
                                        Amount = 2
                                    },
                                    new AccLine
                                    {
                                        ItemId = 3,
                                        LineId = 1,
                                        SubCenterNo = 22222,
                                        Amount = 4
                                    }
                                }
                            },
                        new LineItem
                                                {
                                LineId = 1,
                                BudgetObject = 856,
                                Description = "Description 2",
                                Quantity = 1,
                                RequestId = 1,
                                Unit = "L",
                                UnitPrice = 211.54
                            },
                    },
                Notes = new Note[1]
                    {
                        new Note
                        {
                            NoteId = 1,
                            RequestId = 1,
                            NoteType = "Requester",
                            Content = "This is note of Requester",
                            NoteFiles = new string[] { "814f86f1-3090-495e-bbd6-afbfda293540", "74a16d29-1bf4-480a-8e61-e2ee53fe1070"}
                        }
                    }
            });
            TestRequests.Add(new Request
            {
                RequestNo = 4,
                Requester = "Davit Gogshelidze",
                RequestOffice = "ITS",
                RequestOffice2 = "Main Office",
                RequestDate = DateTime.UtcNow,
                RequestStatus = "Supervisor Approved",
                RequestType = "Contract Service",
                RequiredDeliveryDate = DateTime.UtcNow,
                TechnicalContact = "Tech Content",
                Phone = "+49 123 45678901",
                TechnicalApprovalRequired = true,
                Description = "This is Description For Request Item",
                RenewalContract = true,
                Supplier = "Microsoft",
                LineItems = new LineItem[2]
                    {   new LineItem
                            {
                                LineId = 1,
                                RequestId = 1,
                                BudgetObject = 10000.00,
                                Description = "Description",
                                Quantity = 3,
                                Unit = "KG",
                                UnitPrice = 100.00,
                                AccLines = new AccLine[3]
                                {
                                    new AccLine
                                    {
                                        ItemId = 1,
                                        LineId = 1,
                                        SubCenterNo = 4444,
                                        Amount = 3
                                    },
                                    new AccLine
                                    {
                                        ItemId = 2,
                                        LineId = 1,
                                        SubCenterNo = 11111,
                                        Amount = 2
                                    },
                                    new AccLine
                                    {
                                        ItemId = 3,
                                        LineId = 1,
                                        SubCenterNo = 22222,
                                        Amount = 4
                                    }
                                }
                            },
                        new LineItem
                                                {
                                LineId = 1,
                                BudgetObject = 856,
                                Description = "Description 2",
                                Quantity = 1,
                                RequestId = 1,
                                Unit = "L",
                                UnitPrice = 211.54
                            },
                    },
                Notes = new Note[1]
                    {
                        new Note
                        {
                            NoteId = 1,
                            RequestId = 1,
                            NoteType = "Requester",
                            Content = "This is note of Requester",
                            NoteFiles = new string[] { "814f86f1-3090-495e-bbd6-afbfda293540", "74a16d29-1bf4-480a-8e61-e2ee53fe1070"}
                        }
                    }
            });
            TestRequests.Add(new Request
                {
                    RequestNo = 5,
                    Requester = "Davit Gogshelidze",
                    RequestOffice = "ITS",
                    RequestOffice2 = "Main Office",
                    RequestDate = DateTime.UtcNow,
                    RequestStatus = "Director Approved",
                    RequestType = "Contract Service",
                    RequiredDeliveryDate = DateTime.UtcNow,
                    TechnicalContact = "Tech Content",
                    Phone = "+49 123 45678901",
                    TechnicalApprovalRequired = true,
                    Description = "This is Description For Request Item",
                    RenewalContract = true,
                    Supplier = "Microsoft",
                    LineItems = new LineItem[2]
                    {   new LineItem
                            {
                                LineId = 1,
                                RequestId = 1,
                                BudgetObject = 10000.00,
                                Description = "Description",
                                Quantity = 3,
                                Unit = "KG",
                                UnitPrice = 100.00,
                                AccLines = new AccLine[3]
                                {
                                    new AccLine
                                    {
                                        ItemId = 1,
                                        LineId = 1,
                                        SubCenterNo = 4444,
                                        Amount = 3
                                    },
                                    new AccLine
                                    {
                                        ItemId = 2,
                                        LineId = 1,
                                        SubCenterNo = 11111,
                                        Amount = 2
                                    },
                                    new AccLine
                                    {
                                        ItemId = 3,
                                        LineId = 1,
                                        SubCenterNo = 22222,
                                        Amount = 4
                                    }
                                }
                            },
                        new LineItem
                                                {
                                LineId = 1,
                                BudgetObject = 856,
                                Description = "Description 2",
                                Quantity = 1,
                                RequestId = 1,
                                Unit = "L",
                                UnitPrice = 211.54
                            },
                    },
                    Notes = new Note[1]
                    {
                        new Note
                        {
                            NoteId = 1,
                            RequestId = 1,
                            NoteType = "Requester",
                            Content = "This is note of Requester",
                            NoteFiles = new string[] { "814f86f1-3090-495e-bbd6-afbfda293540", "74a16d29-1bf4-480a-8e61-e2ee53fe1070"}
                        }
                    }
                });
            //});









}

        //    {

        //new Request
        //        {
        //            RequestNo = 2,
        //            Requester = "Davit Gogshelidze",
        //            RequestOffice = "ITS",
        //            RequestOffice2 = "Secondary Office",
        //            RequestDate = DateTime.UtcNow,
        //            RequestStatus = "Creator Approved",
        //            RequestType = "Request Type 2",
        //            RequiredDeliveryDate = DateTime.UtcNow,
        //            TechnicalContact = "Books",
        //            Phone = "+49 123 10987654",
        //            TechnicalApprovalRequired = false,
        //            Description = "22 This is Description For Request Item 2",
        //            RenewalContract = false,
        //            Supplier = "Microsoft",
        //            LineItems = new LineItem[2]
        //            {   new LineItem
        //                    {
        //                        LineId = 1,
        //                        RequestId = 2,
        //                        BudgetObject = 1500.00,
        //                        Description = "this is Description",
        //                        Quantity = 2,
        //                        Unit = "KG",
        //                        UnitPrice = 100.00,
        //                        Amount = 100.00,
        //                        AccLines = new AccLine[3]
        //                        {
        //                            new AccLine
        //                            {
        //                                ItemId = 1,
        //                                LineId = 1,
        //                                SubCenterNo = 4444,
        //                                Amount = 3
        //                            },
        //                            new AccLine
        //                            {
        //                                ItemId = 2,
        //                                LineId = 1,
        //                                SubCenterNo = 11111,
        //                                Amount = 2
        //                            },
        //                            new AccLine
        //                            {
        //                                ItemId = 3,
        //                                LineId = 1,
        //                                SubCenterNo = 22222,
        //                                Amount = 4
        //                            }
        //                        }
        //                    },
        //                new LineItem
        //                                        {
        //                        LineId = 1,
        //                        BudgetObject = 856,
        //                        Description = "Description 2",
        //                        Quantity = 1,
        //                        RequestId = 1,
        //                        Unit = "L",
        //                        UnitPrice = 211.54
        //                    },
        //            },
        //            Notes = new Note[1]
        //            {
        //                new Note
        //                {
        //                    NoteId = 1,
        //                    RequestId = 1,
        //                    NoteType = "Requester",
        //                    Content = "This is note of Requester",
        //                    NoteFiles = new string[] { "814f86f1-3090-495e-bbd6-afbfda293540", "74a16d29-1bf4-480a-8e61-e2ee53fe1070"}
        //                }
        //            }
        //        },
        //        new Request
        //        {
        //            RequestNo = 3,
        //            Requester = "Davit Gogshelidze",
        //            RequestOffice = "ITS",
        //            RequestOffice2 = "Main Office",
        //            RequestDate = DateTime.UtcNow,
        //            RequestStatus = "Request Generated",
        //            RequestType = "Contract Service",
        //            RequiredDeliveryDate = DateTime.UtcNow,
        //            TechnicalContact = "Tech Content",
        //            Phone = "+49 123 45678901",
        //            TechnicalApprovalRequired = true,
        //            Description = "This is Description For Request Item",
        //            RenewalContract = true,
        //            Supplier = "Microsoft",
        //            LineItems = new LineItem[2]
        //            {   new LineItem
        //                    {
        //                        LineId = 1,
        //                        RequestId = 1,
        //                        BudgetObject = 10000.00,
        //                        Description = "Description",
        //                        Quantity = 3,
        //                        Unit = "KG",
        //                        UnitPrice = 100.00,
        //                        AccLines = new AccLine[3]
        //                        {
        //                            new AccLine
        //                            {
        //                                ItemId = 1,
        //                                LineId = 1,
        //                                SubCenterNo = 4444,
        //                                Amount = 3
        //                            },
        //                            new AccLine
        //                            {
        //                                ItemId = 2,
        //                                LineId = 1,
        //                                SubCenterNo = 11111,
        //                                Amount = 2
        //                            },
        //                            new AccLine
        //                            {
        //                                ItemId = 3,
        //                                LineId = 1,
        //                                SubCenterNo = 22222,
        //                                Amount = 4
        //                            }
        //                        }
        //                    },
        //                new LineItem
        //                                        {
        //                        LineId = 1,
        //                        BudgetObject = 856,
        //                        Description = "Description 2",
        //                        Quantity = 1,
        //                        RequestId = 1,
        //                        Unit = "L",
        //                        UnitPrice = 211.54
        //                    },
        //            },
        //            Notes = new Note[1]
        //            {
        //                new Note
        //                {
        //                    NoteId = 1,
        //                    RequestId = 1,
        //                    NoteType = "Requester",
        //                    Content = "This is note of Requester",
        //                    NoteFiles = new string[] { "814f86f1-3090-495e-bbd6-afbfda293540", "74a16d29-1bf4-480a-8e61-e2ee53fe1070"}
        //                }
        //            }
        //        },
        //        new Request
        //        {
        //            RequestNo = 4,
        //            Requester = "Davit Gogshelidze",
        //            RequestOffice = "ITS",
        //            RequestOffice2 = "Main Office",
        //            RequestDate = DateTime.UtcNow,
        //            RequestStatus = "Supervisor Approved",
        //            RequestType = "Contract Service",
        //            RequiredDeliveryDate = DateTime.UtcNow,
        //            TechnicalContact = "Tech Content",
        //            Phone = "+49 123 45678901",
        //            TechnicalApprovalRequired = true,
        //            Description = "This is Description For Request Item",
        //            RenewalContract = true,
        //            Supplier = "Microsoft",
        //            LineItems = new LineItem[2]
        //            {   new LineItem
        //                    {
        //                        LineId = 1,
        //                        RequestId = 1,
        //                        BudgetObject = 10000.00,
        //                        Description = "Description",
        //                        Quantity = 3,
        //                        Unit = "KG",
        //                        UnitPrice = 100.00,
        //                        AccLines = new AccLine[3]
        //                        {
        //                            new AccLine
        //                            {
        //                                ItemId = 1,
        //                                LineId = 1,
        //                                SubCenterNo = 4444,
        //                                Amount = 3
        //                            },
        //                            new AccLine
        //                            {
        //                                ItemId = 2,
        //                                LineId = 1,
        //                                SubCenterNo = 11111,
        //                                Amount = 2
        //                            },
        //                            new AccLine
        //                            {
        //                                ItemId = 3,
        //                                LineId = 1,
        //                                SubCenterNo = 22222,
        //                                Amount = 4
        //                            }
        //                        }
        //                    },
        //                new LineItem
        //                                        {
        //                        LineId = 1,
        //                        BudgetObject = 856,
        //                        Description = "Description 2",
        //                        Quantity = 1,
        //                        RequestId = 1,
        //                        Unit = "L",
        //                        UnitPrice = 211.54
        //                    },
        //            },
        //            Notes = new Note[1]
        //            {
        //                new Note
        //                {
        //                    NoteId = 1,
        //                    RequestId = 1,
        //                    NoteType = "Requester",
        //                    Content = "This is note of Requester",
        //                    NoteFiles = new string[] { "814f86f1-3090-495e-bbd6-afbfda293540", "74a16d29-1bf4-480a-8e61-e2ee53fe1070"}
        //                }
        //            }
        //        },
        //        new Request
        //        {
        //            RequestNo = 5,
        //            Requester = "Davit Gogshelidze",
        //            RequestOffice = "ITS",
        //            RequestOffice2 = "Main Office",
        //            RequestDate = DateTime.UtcNow,
        //            RequestStatus = "Director Approved",
        //            RequestType = "Contract Service",
        //            RequiredDeliveryDate = DateTime.UtcNow,
        //            TechnicalContact = "Tech Content",
        //            Phone = "+49 123 45678901",
        //            TechnicalApprovalRequired = true,
        //            Description = "This is Description For Request Item",
        //            RenewalContract = true,
        //            Supplier = "Microsoft",
        //            LineItems = new LineItem[2]
        //            {   new LineItem
        //                    {
        //                        LineId = 1,
        //                        RequestId = 1,
        //                        BudgetObject = 10000.00,
        //                        Description = "Description",
        //                        Quantity = 3,
        //                        Unit = "KG",
        //                        UnitPrice = 100.00,
        //                        AccLines = new AccLine[3]
        //                        {
        //                            new AccLine
        //                            {
        //                                ItemId = 1,
        //                                LineId = 1,
        //                                SubCenterNo = 4444,
        //                                Amount = 3
        //                            },
        //                            new AccLine
        //                            {
        //                                ItemId = 2,
        //                                LineId = 1,
        //                                SubCenterNo = 11111,
        //                                Amount = 2
        //                            },
        //                            new AccLine
        //                            {
        //                                ItemId = 3,
        //                                LineId = 1,
        //                                SubCenterNo = 22222,
        //                                Amount = 4
        //                            }
        //                        }
        //                    },
        //                new LineItem
        //                                        {
        //                        LineId = 1,
        //                        BudgetObject = 856,
        //                        Description = "Description 2",
        //                        Quantity = 1,
        //                        RequestId = 1,
        //                        Unit = "L",
        //                        UnitPrice = 211.54
        //                    },
        //            },
        //            Notes = new Note[1]
        //            {
        //                new Note
        //                {
        //                    NoteId = 1,
        //                    RequestId = 1,
        //                    NoteType = "Requester",
        //                    Content = "This is note of Requester",
        //                    NoteFiles = new string[] { "814f86f1-3090-495e-bbd6-afbfda293540", "74a16d29-1bf4-480a-8e61-e2ee53fe1070"}
        //                }
        //            }
        //        }
        //    };
        //}

    }
}
