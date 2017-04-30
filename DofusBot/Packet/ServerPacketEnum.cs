namespace DofusBot.Packet
{
    public enum ServerPacketEnum
    {
        ProtocolRequired = 1,
        HelloConnectMessage = 3,
        LoginQueueStatusMessage = 10,
        IdentificationFailedMessage = 20,
        IdentificationSuccessMessage = 22,
        ServerListMessage = 30,
        SelectedServerDataMessage = 42,
        CredentialsAcknowledgementMessage = 6314,
    }
}
