using System;
using System.Windows.Forms;
using TVMEmulator.helpers;
using TVMEmulator.helpers.builders;
using TVMEmulator.helpers.requests;

namespace TVMEmulator.emulation.ADA
{
    internal class AdaMode
    {
        private string custidTB;
        private string passwordTB;
        private string sessionIdTB;

        public static long CustID;
        public static string Password;

        public AdaMode(string custid, string password, string sessionid)
        {
            custidTB = custid;
            passwordTB = password;
            sessionIdTB = sessionid;
        }

        public string GenerateRequest(string action, string adaMessage)
        {
            LinkRequestDALADAMode.MessageID = RandomBuilder.BuildRandomString(7) + "TstHrns";
            LinkRequestDALADAMode.SessionID = sessionIdTB;

            long.TryParse(custidTB, out long tempCustid);
            if (tempCustid <= 0)
            {
                MessageBox.Show("Invalid Custid");
                return "Invalid Custid";
            }
            CustID = tempCustid;
            Password = passwordTB;

            string output = "ERROR";
            if (action.Equals(AdaActionCode.StartAdaMode.GetStringValue()))
            {
                output = LinkRequestDALADAMode.GenerateStartRequest(CustID, Password, adaMessage);
            }
            else if (action.Equals(AdaActionCode.UpdateMessage.GetStringValue()))
            {
                output = LinkRequestDALADAMode.GenerateUpdateMessageRequest(CustID, Password, adaMessage);
            }
            else if (action.Equals(AdaActionCode.EndAdaMode.GetStringValue()))
            {
                output = LinkRequestDALADAMode.GenerateEndRequest(CustID, Password);
            }
            else
            {
                MessageBox.Show($"Action: {action} is not yet implemented.");
            }
            return output;
        }
    }
}
