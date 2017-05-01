using DofusBot.Network;
using DofusBot.Packet;
using DofusBot.Packet.Messages.Connection;
using DofusBot.Packet.Messages.Game.Approach;
using DofusBot.Packet.Messages.Queues;
using DofusBot.Packet.Messages.Security;
using DofusBot.Packet.Types;
using DofusBot.Packet.Types.Connection;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Windows.Forms;

namespace DofusBot.Interface
{
    public partial class Main : Form
    {
        private DofusBotSocket _ServerSocket;
        private DofusBotSocket _GameSocket;
        private DofusBotPacketDeserializer _deserializer;
        private object _ticket;

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
            string Connect = "Connexion";
            string Disconnect = "Deconnexion";

            logTextBox.Text = "";

            Invoke((MethodInvoker)delegate
            {
                if (connectionButton.Text == Connect)
                {
                    if (string.IsNullOrWhiteSpace(accountNameTextBox.Text) || string.IsNullOrWhiteSpace(accountPasswdTextBox.Text))
                        Log(LogMessageType.Administrateurs, "Vous devez rentrer vos identifiants.");
                    else
                    {
                        string DofusIP = "213.248.126.40";
                        int DofusPort = 5555;
                        _ServerSocket = new DofusBotSocket(_deserializer, new IPEndPoint(IPAddress.Parse(DofusIP), DofusPort));
                        Log(LogMessageType.Informations, "Connexion en cours <" + DofusIP + ":" + DofusPort + ">");
                        _ServerSocket.ConnectEndListen();

                        connectionButton.Text = Disconnect;
                    }
                }
                else
                {
                    _ServerSocket.CloseSocket();
                    _ServerSocket = null;
                    
                    if (_GameSocket != null)
                    {
                        _GameSocket.CloseSocket();
                        _GameSocket = null;
                    }

                    Log(LogMessageType.Informations, "Déconnecté.");
                    connectionButton.Text = Connect;
                }
            });             
        }

        public void OnReceivedNullPacket(object source, NullPacketEventArg e)
        {
            Log(LogMessageType.Administrateurs, "Packet: [" + e.PacketType + "] is not implemented.");
        }

        public void OnReceivedPacket(object source, PacketEventArg e)
        {
            ServerPacketEnum packetType = (ServerPacketEnum)e.Packet.MessageID;
            Log(LogMessageType.Administrateurs, "[Serveur] " + packetType.ToString());
            switch (packetType)
            {
                case ServerPacketEnum.ProtocolRequired:
                    break;
                case ServerPacketEnum.CredentialsAcknowledgementMessage:
                    break;
                case ServerPacketEnum.HelloGameMessage:
                    HelloGameMessage helloGame = (HelloGameMessage)e.Packet;
                    AuthenticationTicketMessage ATM = new AuthenticationTicketMessage("fr", _ticket.ToString());
                    _GameSocket.Send(ATM);
                    Log(LogMessageType.Administrateurs, "[Client] " + ATM.PacketType);
                    break;
                case ServerPacketEnum.RawDataMessage:
                    List<int> tt = new List<int>();
                    for (int i = 0; i <= 255; i++)
                    {
                        Random random = new Random();
                        int test = random.Next(-127, 127);
                        tt.Add(test);
                    }
                    CheckIntegrityMessage rawData = new CheckIntegrityMessage(tt);
                    _GameSocket.Send(rawData);
                    Log(LogMessageType.Administrateurs, "[Client] " + rawData.PacketType);
                    break;
                case ServerPacketEnum.HelloConnectMessage:
                    HelloConnectMessage helloConnectMessage = (HelloConnectMessage)e.Packet;
                    sbyte[] credentials = RSA.RSAKey.Encrypt(helloConnectMessage.key, accountNameTextBox.Text, accountPasswdTextBox.Text, helloConnectMessage.salt);
                    VersionExtended version = new VersionExtended(2, 41, 1, 120116, 1, 0, 1, 1);
                    IdentificationMessage idm = new IdentificationMessage(false, false, false, version, "fr", credentials, 0, 0, new ushort[0]);
                    Log(LogMessageType.Informations, "Identification en cours...");
                    _ServerSocket.Send(idm);
                    Log(LogMessageType.Administrateurs, "[Client] " + idm.PacketType);
                    break;
                case ServerPacketEnum.LoginQueueStatusMessage:
                    LoginQueueStatusMessage loginQueueStatusMessage = (LoginQueueStatusMessage)e.Packet;
                    if (loginQueueStatusMessage.Position != 0 && loginQueueStatusMessage.Total != 0)
                        Log(LogMessageType.Informations, "Vous êtes en position " + loginQueueStatusMessage.Position + " sur " + loginQueueStatusMessage.Total + " dans la file d'attente.");
                    break;
                case ServerPacketEnum.IdentificationFailedMessage:
                    Log(LogMessageType.Promotion, "Identification échouée ! Veuillez recommencer.");
                    Invoke((MethodInvoker)delegate
                    {
                        if (_ServerSocket != null)
                        {
                            _ServerSocket.CloseSocket();
                            _ServerSocket = null;
                        }
                        connectionButton.Text = "Connexion";
                    });
                    break;
                case ServerPacketEnum.IdentificationSuccessMessage:
                    IdentificationSuccessMessage idSuccess = (IdentificationSuccessMessage)e.Packet;
                    Log(LogMessageType.Prive, "Bonjour " + idSuccess.Nickname + " !");
                    break;
                case ServerPacketEnum.ServerListMessage:
                    ServerListMessage servers = (ServerListMessage)e.Packet;
                    foreach(GameServerInformations i in servers.Servers ){
                        if (i.CharactersCount > 0 && i.IsSelectable && (ServerStatus)i.Status == ServerStatus.EnLigne)
                        {
                            ServerSelectionMessage SSM = new ServerSelectionMessage(i.ObjectID);
                            _ServerSocket.Send(SSM);
                            Log(LogMessageType.Administrateurs, "[Client] " + SSM.PacketType);
                            break;
                        }

                    }

                    break;
                case ServerPacketEnum.SelectedServerDataMessage:
                    SelectedServerDataMessage selected = (SelectedServerDataMessage)e.Packet;
                    Log(LogMessageType.Informations, "Connecté au serveur : " + selected.ServerId);
                    _ServerSocket.CloseSocket();
                    _ServerSocket = null;
                    _ticket = AES.AES.TicketTrans(selected.Ticket);
                    _GameSocket = new DofusBotSocket(_deserializer, new IPEndPoint(IPAddress.Parse(selected.Address), selected.Port));
                    _GameSocket.ConnectEndListen();
                    break;
                case ServerPacketEnum.SelectedServerDataExtendedMessage:
                    SelectedServerDataExtendedMessage selectedExtended = (SelectedServerDataExtendedMessage)e.Packet;
                    Log(LogMessageType.Informations, "Connecté au serveur : " + selectedExtended.ServerId);
                    _ServerSocket.CloseSocket();
                    _ServerSocket = null;
                    _ticket = AES.AES.TicketTrans(selectedExtended.Ticket);
                    _GameSocket = new DofusBotSocket(_deserializer, new IPEndPoint(IPAddress.Parse(selectedExtended.Address), selectedExtended.Port));
                    _GameSocket.ConnectEndListen();
                    break;
                default:
                    Log(LogMessageType.Administrateurs, "Packet: [" + e.Packet.MessageID + "] is not treated.");
                    break;
            }
        }

        private void MainFormClosing(object sender, FormClosingEventArgs e)
        {
            if (_ServerSocket != null || _GameSocket != null)
            {
                if (MessageBox.Show("Une connexion est en cours. Voulez-vous vraiment fermer le bot? Si oui, la déconnexion s'effectuera automatiquement.", "Attention", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (_ServerSocket != null)
                    {
                        _ServerSocket.CloseSocket();
                        _ServerSocket = null;
                    }
                    if (_GameSocket != null)
                    {
                        _GameSocket.CloseSocket();
                        _GameSocket = null;
                    }
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
