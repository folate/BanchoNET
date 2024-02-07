﻿using BanchoNET.Objects;
using BanchoNET.Objects.Players;
using BanchoNET.Utils;

namespace BanchoNET.Packets;

public partial class ClientPackets
{
	private static void ChangeAction(Player player, BinaryReader br)
	{
		var status = player.Status;

		status.Activity = (Activity)br.ReadByte();
		status.ActivityDescription = br.ReadOsuString();
		status.BeatmapMD5 = br.ReadOsuString();
		status.CurrentMods = (Mods)br.ReadInt32();
		status.Mode = (GameMode)br.ReadByte();
		
		//TODO validate mode
		
		status.BeatmapId = br.ReadInt32();
		
		using var packet = new ServerPackets();
		packet.UserStats(player);
		Session.EnqueueToPlayers(packet.GetContent());
	}
}