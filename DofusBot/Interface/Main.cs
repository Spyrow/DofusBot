using DofusBot.Network;
using DofusBot.Packet;
using DofusBot.Packet.Messages.Connection;
using DofusBot.Packet.Messages.Game.Approach;
using DofusBot.Packet.Messages.Queues;
using DofusBot.Packet.Messages.Security;
using DofusBot.Packet.Types;
using DofusBot.Packet.Types.Connection;
using System;
using System.Drawing;
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
            _deserializer.ReceivePacket += OnReceivedPacket;
            _deserializer.ReceiveNullPacket += OnReceivedNullPacket;

            logTextBox.Font = new Font("Verdana", 8, FontStyle.Regular);
        }

        public enum LogMessageType
        {
            General, Equipe, Guilde, Alliance, Groupe, Commerce, Recrutement, Debutants, Administrateurs, Prive, Informations, Promotion, Kolizeum
        }

        public enum ServerStatus: byte
        {
            Inconnu, HorsLigne, EnCoursDeDémarrage, EnLigne, Inaccessible, EnCoursDeSauvegarde, EnCoursDExtinction, Complet
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
                logTextBox.Select(logTextBox.Text.Length, 0);
                logTextBox.ScrollToCaret();
            };
            this.Invoke(log_callback);
        }

        private void ConnectionButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(accountNameTextBox.Text) || string.IsNullOrWhiteSpace(accountPasswdTextBox.Text))
                Log(LogMessageType.Administrateurs, "Vous devez rentrer vos identifiants.");
            else
            {
                string DofusIP = "213.248.126.40";
                int DofusPort = 5555;
                _socket = new DofusBotSocket(_deserializer, new IPEndPoint(IPAddress.Parse(DofusIP), DofusPort));
                Log(LogMessageType.Informations, "Connexion en cours <" + DofusIP + ":" + DofusPort +">");
                _socket.ConnectEndListen();
            }                
        }

        public void OnReceivedNullPacket(object source, NullPacketEventArg e)
        {
            Log(LogMessageType.Administrateurs, "Packet: [" + e.PacketType + "] is not implemented");
        }

        public void OnReceivedPacket(object source, PacketEventArg e)
        {
            ServerPacketEnum packetType = (ServerPacketEnum)e.Packet.MessageID;

            switch (packetType)
            {
                case ServerPacketEnum.ProtocolRequired:
                    break;
                case ServerPacketEnum.CredentialsAcknowledgementMessage:
                    break;
                case ServerPacketEnum.HelloGameMessage:
                    HelloGameMessage helloGame = (HelloGameMessage)e.Packet;
                    Log(LogMessageType.Administrateurs, "HelloGameMessage");
                    break;
                case ServerPacketEnum.RawDataMessage:
                    RawDataMessage rawData = (RawDataMessage)e.Packet;
                    Log(LogMessageType.Administrateurs, rawData.Content.ToString());
                    break;
                case ServerPacketEnum.HelloConnectMessage:
                    HelloConnectMessage helloConnectMessage = (HelloConnectMessage)e.Packet;
                    sbyte[] credentials = RSA.RSAKey.Encrypt(helloConnectMessage.key, accountNameTextBox.Text, accountPasswdTextBox.Text, helloConnectMessage.salt);
                    VersionExtended version = new VersionExtended(2, 41, 1, 120116, 1, 0, 1, 1);
                    IdentificationMessage idm = new IdentificationMessage(false, false, false, version, "fr", credentials, 0, 0, new ushort[0]);
                    Log(LogMessageType.Informations, "Identification en cours...");
                    _socket.Send(idm);
                    break;
                case ServerPacketEnum.LoginQueueStatusMessage:
                    LoginQueueStatusMessage loginQueueStatusMessage = (LoginQueueStatusMessage)e.Packet;
                    Log(LogMessageType.Informations, "Vous êtes en position " + loginQueueStatusMessage.position + " sur " + loginQueueStatusMessage.total + " dans la file d'attente.");
                    break;
                case ServerPacketEnum.IdentificationFailedMessage:
                    Log(LogMessageType.Administrateurs, "Identification échoué ! Veuillez recommencer.");
                    break;
                case ServerPacketEnum.IdentificationSuccessMessage:
                    IdentificationSuccessMessage idSuccess = (IdentificationSuccessMessage)e.Packet;
                    Log(LogMessageType.Prive, "Bonjour " + idSuccess.Nickname + " !");
                    break;
                case ServerPacketEnum.ServerListMessage:
                    ServerListMessage servers = (ServerListMessage)e.Packet;
                    if (servers.AlreadyConnectedToServerId != 0)
                    {
                        ServerSelectionMessage serverSelection = new ServerSelectionMessage(servers.AlreadyConnectedToServerId);
                        _socket.Send(serverSelection);
                        break;
                    }

                    for (int i = 0; i < servers.Servers.Count; i++)
                    {
                        GameServerInformations serverInfos = servers.Servers[i];
                        if (serverInfos.CharactersCount >= 1)
                        {
                            if (serverInfos.IsSelectable && (ServerStatus)serverInfos.Status == ServerStatus.EnLigne)
                            {
                                ServerSelectionMessage serverSelection = new ServerSelectionMessage(serverInfos.ObjectID);
                                _socket.Send(serverSelection);
                            }
                            else
                                Log(LogMessageType.Administrateurs, "Veuillez patienter.. Le statut de votre serveur est : " + (ServerStatus)serverInfos.Status);
                        }
                    }
                    break;
                case ServerPacketEnum.SelectedServerDataMessage:
                    SelectedServerDataMessage selected = (SelectedServerDataMessage)e.Packet;
                    Log(LogMessageType.Informations, "Connecté au serveur : " + selected.ServerId);
                    AuthenticationTicketMessage authenticationTicket = new AuthenticationTicketMessage("fr", Encoding.UTF8.GetString(selected.Ticket.ToArray()));
                    _socket = new DofusBotSocket(_deserializer, new IPEndPoint(IPAddress.Parse(selected.Address), selected.Port));
                    _socket.ConnectEndListen();
                    _socket.Send(authenticationTicket);
                    break;
                case ServerPacketEnum.SelectedServerDataExtendedMessage:
                    SelectedServerDataExtendedMessage selectedExtended = (SelectedServerDataExtendedMessage)e.Packet;
                    Log(LogMessageType.Informations, "Connecté au serveur : " + selectedExtended.ServerId);
                    AuthenticationTicketMessage authenticationTicket2 = new AuthenticationTicketMessage("fr", Encoding.UTF8.GetString(selectedExtended.Ticket.ToArray()));
                    _socket = new DofusBotSocket(_deserializer, new IPEndPoint(IPAddress.Parse(selectedExtended.Address), selectedExtended.Port));
                    _socket.ConnectEndListen();
                    _socket.Send(authenticationTicket2);
                    break;
                default:
                    Log(LogMessageType.Administrateurs, "Packet id : {" + e.Packet.MessageID + "} is not treated.");
                    break;
            }
        }
    }
}
