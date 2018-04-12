﻿/*
 * Copyright (c) 2018 Demerzel Solutions Limited
 * This file is part of the Nethermind library.
 *
 * The Nethermind library is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * The Nethermind library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with the Nethermind. If not, see <http://www.gnu.org/licenses/>.
 */

using System.Numerics;
using Nethermind.Core;
using Nethermind.Core.Crypto;

namespace Nethermind.Blockchain
{
    public interface IBlockTree
    {
        Keccak GenesisHash { get; } // TODO: move from processor
        IChain MainChain { get; set; } // TODO: move from processor

        void AddBlock(Block block);
        Block FindBlock(Keccak blockHash, bool mainChainOnly);
        Block[] FindBlocks(Keccak blockHash, int numberOfBlocks, int skip, bool reverse);
        Block FindBlock(BigInteger blockNumber);
        bool IsMainChain(Keccak blockHash);
        void MoveToMain(Keccak blockHash);
        void MoveToBranch(Keccak blockHash);
        bool WasProcessed(Keccak blockHash);
        void MarkAsProcessed(Keccak blockHash);
    }
}