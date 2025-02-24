using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Net;
using System.Net.Sockets;
using System.Net.Security;
using System.Text;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Windows.Forms;

namespace OfflineRetailV2
{
    public class SslTcpClient
    {
        private static Hashtable certificateErrors = new Hashtable();
        // The following method is invoked by the RemoteCertificateValidationDelegate.
        public static bool ValidateServerCertificate(
              object sender,
              X509Certificate certificate,
              X509Chain chain,
              SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            //Console.WriteLine("Certificate error: {0}", sslPolicyErrors);

            // Do not allow this client to communicate with unauthenticated servers.
            return false;
        }

        public static void RunClient(string POSLynx, int port, XmlDocument xmlRequest, ref XmlDocument xmlResponse)
        {
            // Create a TCP/IP client socket.
            // machineName is the host running the server application.

            string machineName = POSLynx + ".poslynx.org";

            TcpClient client = new TcpClient(machineName, port);

            //Console.WriteLine("Client connected.");
            // Create an SSL stream that will close the client's stream.
            SslStream sslStream = new SslStream(client.GetStream(), false, new RemoteCertificateValidationCallback(ValidateServerCertificate), null);
            // The server name must match the name on the server certificate.

            //X509Store store = new X509Store(StoreName.My);
            //store.Open(OpenFlags.ReadWrite);


            //X509Certificate2Collection certs = store.Certificates.Find(X509FindType.FindBySubjectName, "MyServer", false); // vaildOnly = true

            X509CertificateCollection certs = new X509CertificateCollection();

            string certpath = Application.StartupPath;
            if (!(certpath.EndsWith("\\"))) certpath = certpath + "\\";
            certpath = certpath + "testssl.cer";

            X509Certificate cert = X509Certificate.CreateFromCertFile(certpath);
            certs.Add(cert);
            try
            {
                //sslStream.AuthenticateAsClient("0B21B4.poslynx.org", certs, SslProtocols.Tls, false);
                sslStream.AuthenticateAsClient(machineName, certs, SslProtocols.Tls, false); //SERVER
            }
            catch (AuthenticationException e)
            {
                //Console.WriteLine("Exception: {0}", e.Message);
                if (e.InnerException != null)
                {
                    //Console.WriteLine("Inner exception: {0}", e.InnerException.Message);
                }
                //Console.WriteLine("Authentication failed - closing the connection.");
                client.Close();
                System.Threading.Thread.Sleep(150);
                return;
            }
            // Encode a test message into a byte array.
            // Signal the end of the message using the "<EOF>".
            //REQUEST FILE CONTENT

            string filecontent = xmlRequest.OuterXml;
            byte[] messsage = Encoding.UTF8.GetBytes(filecontent + "<EOF>"); //EOF
            // Send hello message to the server. 
            sslStream.Write(messsage);
            sslStream.Flush();
            // Read message from the server.
            string serverMessage = ReadMessage(sslStream); //RESPONSE 

            xmlResponse.LoadXml(serverMessage);
            //Console.WriteLine("Server says: {0}", serverMessage);

            // Close the client connection.
            client.Close();

            System.Threading.Thread.Sleep(150);

            //Console.WriteLine("Client closed.");
        }

        static string ReadMessage(SslStream sslStream)
        {
            // Read the  message sent by the server.
            // The end of the message is signaled using the
            // "<EOF>" marker.
            byte[] buffer = new byte[2048];
            StringBuilder messageData = new StringBuilder();
            int bytes = -1;
            do
            {
                bytes = sslStream.Read(buffer, 0, buffer.Length);

                // Use Decoder class to convert from bytes to UTF8
                // in case a character spans two buffers.
                Decoder decoder = Encoding.UTF8.GetDecoder();
                char[] chars = new char[decoder.GetCharCount(buffer, 0, bytes)];
                decoder.GetChars(buffer, 0, bytes, chars, 0);
                messageData.Append(chars);
                // Check for EOF.
                if (messageData.ToString().IndexOf("<EOF>") != -1)
                {
                    break;
                }
            } while (bytes != 0);

            return messageData.ToString();
        }
    }
}
