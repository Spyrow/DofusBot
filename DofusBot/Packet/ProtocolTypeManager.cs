using DofusBot.Core.Network;
using DofusBot.Packet.Types.Connection;
using DofusBot.Packet.Types.Game.Approach;
using DofusBot.Packet.Types.Game.Character;
using DofusBot.Packet.Types.Game.Character.Choice;
using DofusBot.Packet.Types.Game.Context.Roleplay;
using DofusBot.Packet.Types.Game.Look;
using DofusBot.Packet.Types.Game.Social;
using System;
using System.Collections.Generic;

namespace DofusBot.Packet
{
    public class ProtocolTypeManager
    {
        // Fields
        private static bool v_IsInitialised { get; set; }
        private static Dictionary<int, Type> v_Id_Instance { get; set; }
        public static Type cast;
        // Methods
        public static object GetInstance(int Id)
        {
            if (!ProtocolTypeManager.v_IsInitialised)
            {
                ProtocolTypeManager.InitInstance();
            }
            Console.WriteLine("Id de l'instance : " + Id);
            try
            {
                return (NetworkMessage)Activator.CreateInstance(ProtocolTypeManager.v_Id_Instance[Id]);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return (NetworkMessage)Activator.CreateInstance(ProtocolTypeManager.v_Id_Instance[Id]);
        }

        public static void InitInstance()
        {
            v_IsInitialised = true;

            ProtocolTypeManager.v_Id_Instance.Add(Types.Version.ProtocolId, typeof(Types.Version));
            ProtocolTypeManager.v_Id_Instance.Add(GameServerInformations.ProtocolId, typeof(GameServerInformations));
            ProtocolTypeManager.v_Id_Instance.Add(CharacterBaseInformations.ProtocolId, typeof(CharacterBaseInformations));
            ProtocolTypeManager.v_Id_Instance.Add(SubEntity.ProtocolId, typeof(SubEntity));
            ProtocolTypeManager.v_Id_Instance.Add(EntityLook.ProtocolId, typeof(EntityLook));
            ProtocolTypeManager.v_Id_Instance.Add(CharacterMinimalInformations.ProtocolId, typeof(CharacterMinimalInformations));
            ProtocolTypeManager.v_Id_Instance.Add(CharacterMinimalPlusLookInformations.ProtocolId, typeof(CharacterMinimalPlusLookInformations));
            ProtocolTypeManager.v_Id_Instance.Add(CharacterMinimalPlusLookAndGradeInformations.ProtocolId, typeof(CharacterMinimalPlusLookAndGradeInformations));
            ProtocolTypeManager.v_Id_Instance.Add(BasicGuildInformations.ProtocolId, typeof(BasicGuildInformations));
            ProtocolTypeManager.v_Id_Instance.Add(Types.VersionExtended.ProtocolId, typeof(Types.VersionExtended));
            ProtocolTypeManager.v_Id_Instance.Add(AbstractCharacterInformation.ProtocolId, typeof(AbstractCharacterInformation));
            ProtocolTypeManager.v_Id_Instance.Add(AbstractSocialGroupInfos.ProtocolId, typeof(AbstractSocialGroupInfos));
            ProtocolTypeManager.v_Id_Instance.Add(BasicAllianceInformations.ProtocolId, typeof(BasicAllianceInformations));
            ProtocolTypeManager.v_Id_Instance.Add(ServerSessionConstant.ProtocolId, typeof(ServerSessionConstant));
            ProtocolTypeManager.v_Id_Instance.Add(CharacterMinimalAllianceInformations.ProtocolId, typeof(CharacterMinimalAllianceInformations));
            ProtocolTypeManager.v_Id_Instance.Add(CharacterMinimalGuildInformations.ProtocolId, typeof(CharacterMinimalGuildInformations));
            ProtocolTypeManager.v_Id_Instance.Add(CharacterBasicMinimalInformations.ProtocolId, typeof(CharacterBasicMinimalInformations));
        }

    }
}
