namespace DofusBot.Protocol.Network.Messages.Game.Approach
{
    public class AuthenticationTicketAcceptedMessage : NetworkMessage
    {
        public const int ProtocolId = 111;
        public override int MessageID { get { return ProtocolId; } }

        public AuthenticationTicketAcceptedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            //
        }

        public override void Deserialize(IDataReader reader)
        {
            //
        }
    }
}
