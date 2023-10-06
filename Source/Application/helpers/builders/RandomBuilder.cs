using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography;
using TVMEmulator.api;
using TVMEmulator.api.DAL;
using TVMEmulator.api.Payment;
using TVMEmulator.api.Session;
using TVMEmulator.api.Workflows;

namespace TVMEmulator.helpers.builders
{
    public static class RandomBuilder
    {
        private static readonly Random rnd = new Random();
        private static readonly string defaultLicenseKey = ConfigurationManager.AppSettings["LicenseKey"] ?? BuildRandomString(16);
        private static readonly string alphaChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        public static string BuildRandomString(int string_length)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                int bit_count = string_length * 6;
                int byte_count = (bit_count + 7) / 8; // rounded up
                byte[] bytes = new byte[byte_count];
                rng.GetBytes(bytes);
                return Convert.ToBase64String(bytes).TrimEnd('=');
            }
        }

        public static string BuildRandomString(int minLength, int maxLength) => BuildRandomString(rnd.Next(minLength, maxLength));

        public static string BuildRandomAlphaString(int length)
        {
            var stringChars = new char[length];

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = alphaChars[rnd.Next(alphaChars.Length)];
            }

            return new string(stringChars);
        }

        public static int BuildRandomInt(int numDigits)
        {
            int output = rnd.Next((int)Math.Pow(10.0, numDigits - 1), (int)(Math.Pow(10.0, numDigits) - 1));
            return output;
        }

        public static LinkRequest BuildLinkPaymentRequest(bool buildLocal = false)
        {
            return new LinkRequest
            {
                MessageID = BuildRandomString(rnd.Next(5, 16)),
                TCCustID = rnd.Next(1000000, 1999999),
                TCPassword = BuildRandomString(rnd.Next(8, 16)),
                IPALicenseKey = defaultLicenseKey,
                Actions = new List<LinkActionRequest>
                {
                    new LinkActionRequest
                    {
                        MessageID = BuildRandomString(rnd.Next(5, 16)),
                        Action = LinkAction.Payment,
                        PaymentRequest = new LinkPaymentRequest
                        {
                            Demo = true,
                            RequestedAmount = rnd.Next(1,10000),
                            CurrencyCode = "USD",
                            PaymentType = LinkPaymentRequestType.Sale,
                            WorkflowControls = new LinkWorkflowControls
                            {
                                CardEnabled = true
                            },
                            ReferenceInformation = Utilities.ReferenceInformation(true),
                            RequestedTenderType = LinkPaymentRequestedTenderType.Card,
                            PaymentAttributes = new LinkPaymentAttributes
                            {
                                PartialPayment = true
                            },
                            CardWorkflowControls = new LinkCardWorkflowControls
                            {
                                DebitEnabled = true,
                                AVSEnabled = true,
                            },
                            IIASRequest = new LinkIIASRequest
                            {
                                HealthCareAmount = 0,
                                PrescriptionAmount = 0,
                                ClinicAmount = 0,
                                DentalAmount = 0,
                                VisionAmount = 0
                            }
                        },
                        DALRequest = buildLocal ? null : PopulateMockDALIdentifier(null, false)
                    }
                }
            };
        }

        public static LinkRequest BuildLinkUpdateCancelRequest(bool buildLocal = false)
        {
            return new LinkRequest
            {
                MessageID = BuildRandomString(rnd.Next(5, 16)),
                TCCustID = rnd.Next(1000000, 1999999),
                TCPassword = BuildRandomString(rnd.Next(8, 16)),
                IPALicenseKey = defaultLicenseKey,
                Actions = new List<LinkActionRequest>
                {
                    new LinkActionRequest
                    {
                        MessageID = BuildRandomString(rnd.Next(5, 16)),
                        Action = LinkAction.PaymentUpdate,
                        PaymentUpdateRequest = new LinkPaymentUpdateRequest()
                        {
                            RequestedAmount = 0,
                            CancelPayment = true,
                            CancelTransactions = true,
                            ReferenceInformation = Utilities.ReferenceInformation(false)
                        },
                        DALRequest = buildLocal ? null : PopulateMockDALIdentifier(null, false)
                    }
                }
            };
        }

        public static LinkRequest BuildLinkPaymentVoidRequest(LinkPaymentRequestType linkPaymentRequestType)
        {
            return new LinkRequest
            {
                MessageID = BuildRandomString(rnd.Next(5, 16)),
                TCCustID = rnd.Next(1000000, 1999999),
                TCPassword = BuildRandomString(rnd.Next(8, 16)),
                IPALicenseKey = defaultLicenseKey,
                Actions = new List<LinkActionRequest>
                {
                    new LinkActionRequest
                    {
                        MessageID = BuildRandomString(rnd.Next(5, 16)),
                        Action = LinkAction.Payment,
                        PaymentRequest = new LinkPaymentRequest()
                        {
                            RequestedAmount = 0,
                            PaymentType = linkPaymentRequestType
                        }
                    }
                }
            };
        }

        public static string BuildLinkPaymentRequestString(bool buildLocal = false)
        {
            return JsonConvert.SerializeObject(BuildLinkPaymentRequest(buildLocal));
        }

        public static LinkRequest BuildLinkDALStatusRequest(bool buildLocal = false)
        {
            return new LinkRequest
            {
                MessageID = BuildRandomString(rnd.Next(5, 16)),
                TCCustID = rnd.Next(1000000, 1999999),
                TCPassword = BuildRandomString(rnd.Next(8, 16)),
                IPALicenseKey = defaultLicenseKey,
                Actions = new List<LinkActionRequest>
                {
                    new LinkActionRequest
                    {
                        MessageID = BuildRandomString(rnd.Next(5, 16)),
                        Action = LinkAction.DALAction,
                        DALActionRequest = new LinkDALActionRequest
                        {
                            DALAction = LinkDALActionType.GetStatus
                        },
                        DALRequest = buildLocal ? new LinkDALRequest() : PopulateMockDALIdentifier(null, false)
                    }
                }
            };
        }

        public static string BuildLinkDALStatusRequestString(bool buildLocal = false)
        {
            return JsonConvert.SerializeObject(BuildLinkDALStatusRequest(buildLocal));
        }

        private static LinkDALIdentifier MockDALIdentifier(LinkDALIdentifier dalIdentifier)
        {
            if (dalIdentifier != null)
                return dalIdentifier;

            dalIdentifier = new LinkDALIdentifier
            {
                DnsName = "Host" + rnd.Next(0, 999).ToString(),
                IPv4 = rnd.Next(193, 254).ToString() + "." + rnd.Next(193, 254).ToString() + "." + rnd.Next(193, 254).ToString() + "." + rnd.Next(193, 254).ToString(),
                Username = "User" + BuildRandomString(6)
            };

            return dalIdentifier;
        }

        public static LinkDALRequest PopulateMockDALIdentifier(LinkDALRequest linkDALRequest, bool addLookupPreference = true)
        {
            if (linkDALRequest == null)
            {
                linkDALRequest = new LinkDALRequest();
            }

            if (linkDALRequest.DALIdentifier == null)
            {
                linkDALRequest.DALIdentifier = MockDALIdentifier(linkDALRequest.DALIdentifier);
            }

            if (addLookupPreference && (linkDALRequest.DALIdentifier.LookupPreference == null))
            {
                linkDALRequest.DALIdentifier.LookupPreference = LinkDALLookupPreference.WorkstationName;
            }

            return linkDALRequest;
        }

        public static LinkRequest BuildLinkSessionRequest(bool buildLocal = false)
        {
            return new LinkRequest
            {
                MessageID = BuildRandomString(rnd.Next(5, 16)),
                TCCustID = rnd.Next(1000000, 1999999),
                TCPassword = BuildRandomString(rnd.Next(8, 16)),
                IPALicenseKey = defaultLicenseKey,
                Actions = new List<LinkActionRequest>
                {
                    new LinkActionRequest
                    {
                        MessageID = BuildRandomString(rnd.Next(5, 16)),
                        Action = LinkAction.Session,
                        DALRequest = buildLocal ? null : PopulateMockDALIdentifier(null, false),
                        SessionRequest = new LinkSessionRequest
                        {
                            SessionAction = LinkSessionActionType.Initialize,
                            IdleActions = new List<LinkActionRequest>
                            {
                                new LinkActionRequest
                                {
                                    MessageID = BuildRandomString(rnd.Next(5, 16)),
                                    Action = LinkAction.DALAction,
                                    DALActionRequest = new LinkDALActionRequest
                                    {
                                        DALAction = LinkDALActionType.DeviceUI,
                                        DeviceUIRequest = new LinkDeviceUIRequest
                                        {
                                            UIAction = LinkDeviceUIActionType.KeyRequest,
                                            AutoConfirmKey = true,
                                            ReportCardPresented = true,
                                            MinLength = 1,
                                            MaxLength = 1
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }

        public static LinkRequest BuildLinkAdaModeRequest(bool buildLocal = false, bool startAda = true)
        {
            return new LinkRequest
            {
                MessageID = BuildRandomString(rnd.Next(5, 16)),
                TCCustID = rnd.Next(1000000, 1999999),
                TCPassword = BuildRandomString(rnd.Next(8, 16)),
                IPALicenseKey = defaultLicenseKey,
                Actions = new List<LinkActionRequest>
                {
                    new LinkActionRequest
                    {
                        MessageID = BuildRandomString(rnd.Next(5, 16)),
                        Action = LinkAction.DALAction,
                        DALActionRequest = new LinkDALActionRequest
                        {
                            DALAction = startAda ? LinkDALActionType.StartADAMode : LinkDALActionType.EndADAMode
                        },
                        DALRequest = buildLocal ? null : PopulateMockDALIdentifier(null, false)
                    }
                }
            };
        }

        public static LinkRequest BuildLinkDeviceUIRequest(bool buildLocal = false)
        {
            return new LinkRequest
            {
                MessageID = BuildRandomString(rnd.Next(5, 16)),
                TCCustID = rnd.Next(1000000, 1999999),
                TCPassword = BuildRandomString(rnd.Next(8, 16)),
                IPALicenseKey = defaultLicenseKey,
                Actions = new List<LinkActionRequest>
                {
                    new LinkActionRequest
                    {
                        MessageID = BuildRandomString(rnd.Next(5, 16)),
                        Action = LinkAction.DALAction,
                        DALActionRequest = new LinkDALActionRequest
                        {
                            DALAction = LinkDALActionType.DeviceUI
                        },
                        DALRequest = buildLocal ? null : PopulateMockDALIdentifier(null, false)
                    }
                }
            };
        }

        #region PreSwipe
        public static LinkRequest BuildLinkHmacModeRequest(bool buildLocal, bool startHmac)
        {
            return new LinkRequest
            {
                MessageID = BuildRandomString(rnd.Next(5, 16)),
                TCCustID = rnd.Next(1000000, 1999999),
                TCPassword = BuildRandomString(rnd.Next(8, 16)),
                IPALicenseKey = defaultLicenseKey,
                Actions = new List<LinkActionRequest>
                {
                    new LinkActionRequest
                    {
                        MessageID = BuildRandomString(rnd.Next(5, 16)),
                        Action = LinkAction.DALAction,
                        DALActionRequest = new LinkDALActionRequest
                        {
                            DALAction = startHmac ? LinkDALActionType.StartPreSwipeMode: LinkDALActionType.EndPreSwipeMode
                        },
                        DALRequest = buildLocal ? null : PopulateMockDALIdentifier(null, false)
                    }
                }
            };
        }

        public static LinkRequest BuildLinkPreSwipeModeRequest(LinkDALActionType linkDALAction)
        {
            return new LinkRequest
            {
                MessageID = BuildRandomString(rnd.Next(5, 16)),
                TCCustID = rnd.Next(1000000, 1999999),
                TCPassword = BuildRandomString(rnd.Next(8, 16)),
                IPALicenseKey = defaultLicenseKey,
                Actions = new List<LinkActionRequest>
                {
                    new LinkActionRequest
                    {
                        MessageID = BuildRandomString(rnd.Next(5, 16)),
                        Action = LinkAction.DALAction,
                        DALActionRequest = new LinkDALActionRequest
                        {
                            DALAction = linkDALAction
                        },
                        DALRequest = PopulateMockDALIdentifier(null, false)
                    }
                }
            };
        }
        #endregion
    }

}
