using DofusBot.Network;
using DofusBot.Packet;
using DofusBot.Packet.Messages.Connection;
using DofusBot.Packet.Messages.Queues;
using DofusBot.Packet.Types;
using DofusBot.Packet.Types.Connection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace DofusBot.Interface
{
    public partial class Main : Form
    {
        private DofusBotSocket _socket;
        private DofusBotPacketDeserializer _deserializer;

        public Main()
        {
            InitializeComponent();
            _deserializer = new DofusBotPacketDeserializer();
            _deserializer.ReceivePacket += this.OnReceivedPacket;

            logTextBox.Font = new Font("Verdana", 8, FontStyle.Regular);
        }

        public enum LogMessageType
        {
            General, Equipe, Guilde, Alliance, Groupe, Commerce, Recrutement, Debutants, Administrateurs, Prive, Informations, Promotion, Kolizeum, 
        }

        private void Log(LogMessageType type, string Text)
        {
            Action log_callback = delegate
            {
                Console.WriteLine(Text);

                switch (type)
                {
                    case LogMessageType.General:
                        logTextBox.SelectionColor = Color.FromArgb(1, 233, 233, 233);
                        break;
                    case LogMessageType.Equipe:
                        logTextBox.SelectionColor = Color.FromArgb(1, 255, 0, 110);
                        break;
                    case LogMessageType.Guilde:
                        logTextBox.SelectionColor = Color.FromArgb(1, 151, 94, 250);
                        break;
                    case LogMessageType.Alliance:
                        logTextBox.SelectionColor = Color.FromArgb(1, 255, 172, 61);
                        break;
                    case LogMessageType.Groupe:
                        logTextBox.SelectionColor = Color.FromArgb(1, 7, 223, 255);
                        break;
                    case LogMessageType.Commerce:
                        logTextBox.SelectionColor = Color.FromArgb(1, 176, 118, 63);
                        break;
                    case LogMessageType.Recrutement:
                        logTextBox.SelectionColor = Color.FromArgb(1, 230, 159, 213);
                        break;
                    case LogMessageType.Debutants:
                        logTextBox.SelectionColor = Color.FromArgb(1, 213, 169, 14);
                        break;
                    case LogMessageType.Administrateurs:
                        logTextBox.SelectionColor = Color.FromArgb(1, 254, 0, 252);
                        break;
                    case LogMessageType.Prive:
                        logTextBox.SelectionColor = Color.FromArgb(1, 122, 195, 255);
                        break;
                    case LogMessageType.Informations:
                        logTextBox.SelectionColor = Color.FromArgb(1, 72, 164, 33);
                        break;
                    case LogMessageType.Promotion:
                        logTextBox.SelectionColor = Color.FromArgb(1, 235, 61, 68);
                        break;
                    case LogMessageType.Kolizeum:
                        logTextBox.SelectionColor = Color.FromArgb(1, 231, 137, 15);
                        break;
                    default:
                        logTextBox.SelectionColor = Color.Black;
                        break;
                }

                logTextBox.AppendText("[");
                logTextBox.AppendText(DateTime.Now.ToLongTimeString());
                logTextBox.AppendText("] " + Text + "\r\n");
                logTextBox.SelectionColor = logTextBox.ForeColor;
                logTextBox.Select(logTextBox.Text.Length, 0); // On place le curseur à la fin de la zone de texte.
                logTextBox.ScrollToCaret(); // On descend la barre de défilement jusqu'au curseur.
            };
            this.Invoke(log_callback);
        }

        private void ConnectionButton_Click(object sender, EventArgs e)
        {
            if (accountNameTextBox.Text != "" && accountPasswdTextBox.Text != "")
            {
                _socket = new DofusBotSocket(_deserializer, new IPEndPoint(IPAddress.Parse("213.248.126.40"), 5555));
                _socket.ConnectEndListen();
            }
            else
                Log(LogMessageType.Administrateurs, "You have to put your account informations above in order to connect...");
        }

        public void OnReceivedPacket(object source, PacketEventArg e)
        {
            if (e.Packet == null)
                Log(LogMessageType.Administrateurs, "Packet Null");

            ServerPacketEnum packetType = (ServerPacketEnum) e.Packet.MessageID;

            switch (packetType)
            {
                case ServerPacketEnum.ProtocolRequired:
                    break;
                case ServerPacketEnum.HelloConnectMessage:
                    HelloConnectMessage helloConnectMessage = (HelloConnectMessage)e.Packet;
                    sbyte[] credentials = RSA.RSAKey.Encrypt(helloConnectMessage.key, accountNameTextBox.Text, accountPasswdTextBox.Text, helloConnectMessage.salt);
                    VersionExtended version = new VersionExtended(2, 41, 1, 120116, 1, 0, 1, 1);
                    IdentificationMessage idm = new IdentificationMessage(false, false, false, version, "fr", credentials, 0, 0, new ushort[0]);
                    _socket.Send(idm);
                    break;
                case ServerPacketEnum.LoginQueueStatusMessage:
                    LoginQueueStatusMessage loginQueueStatusMessage = (LoginQueueStatusMessage)e.Packet;
                    Log(LogMessageType.Informations, "Waiting queue... " + loginQueueStatusMessage.position + " / " + loginQueueStatusMessage.total);
                    break;
                case ServerPacketEnum.IdentificationFailedMessage:
                    Log(LogMessageType.Administrateurs, "Identification Failed! Try Again..");
                    break;
                case ServerPacketEnum.IdentificationSuccessMessage:
                    IdentificationSuccessMessage idSuccess = (IdentificationSuccessMessage)e.Packet;
                    Log(LogMessageType.Prive, "Bonjour " + idSuccess.Nickname + " !");
                    break;
                case ServerPacketEnum.ServerListMessage:
                    ServerListMessage servers = (ServerListMessage)e.Packet;
                    Log(LogMessageType.Kolizeum, "CanCreateNewCharacter: " + servers.CanCreateNewCharacter);
                    Log(LogMessageType.Kolizeum, "AlreadyConnectedToServerId: " + servers.AlreadyConnectedToServerId);
                    for (int i = 0; i < servers.Servers.Count; i++)
                    {
                        GameServerInformations serverInfos = servers.Servers[i];
                        Log(LogMessageType.Kolizeum, "[Server] ID:" + serverInfos.ObjectID + " Status: " + serverInfos.Status + " IsSelectable: " + serverInfos.IsSelectable + " Completion: " + serverInfos.Completion + " | [" + serverInfos.CharactersCount + "/" + serverInfos.CharactersSlots + "] Characters.");
                    }
                    break;
                default:
                    Log(LogMessageType.Administrateurs, "Packet id : {" + e.Packet.MessageID + "} is not implemented");
                    break;
            }
        }
    }
}
