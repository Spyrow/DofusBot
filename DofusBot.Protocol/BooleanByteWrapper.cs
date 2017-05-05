﻿#region License GNU GPL
// BooleanByteWrapper.cs
// 
// Copyright (C) 2012 - BehaviorIsManaged
// 
// This program is free software; you can redistribute it and/or modify it 
// under the terms of the GNU General Public License as published by the Free Software Foundation;
// either version 2 of the License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; 
// without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. 
// See the GNU General Public License for more details. 
// You should have received a copy of the GNU General Public License along with this program; 
// if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
#endregion
using System;

namespace DofusBot.Protocol
{
    public static class BooleanByteWrapper
    {
        public static byte SetFlag(byte flag, byte offset, bool value)
        {
            if (offset >= 8)
                throw new ArgumentException("offset must be lesser than 8");

            return value ? (byte)(flag | (1 << offset)) : (byte)(flag & 255 - (1 << offset));
        }

        public static byte SetFlag(int flag, byte offset, bool value)
        {
            if (offset >= 8)
                throw new ArgumentException("offset must be lesser than 8");

            return value ? (byte)(flag | (1 << offset)) : (byte)(flag & 255 - (1 << offset));
        }

        public static bool GetFlag(byte flag, byte offset)
        {
            if (offset >= 8)
                throw new ArgumentException("offset must be lesser than 8");

            return (flag & (byte)(1 << offset)) != 0;
        }
    }
}