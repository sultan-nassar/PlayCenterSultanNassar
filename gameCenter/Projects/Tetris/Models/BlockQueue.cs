using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameCenter.Projects.Tetris.Models
{
    public class BlockQueue //respocsible for picking the next block in the game
    {
        private readonly BlockShape[] blocks = new BlockShape[]
       {
            new IBlock(),
            new JBlock(),
            new LBlock(),
            new OBlock(),
            new SBlock(),
            new TBlock(),
            new ZBlock()
       };

        private readonly Random random = new Random();
        public BlockShape NextBlock { get; private set; } 

        public BlockQueue() 
        {
            NextBlock = RandomBlock();
        }

        private BlockShape RandomBlock() // method which return a random block 
        {
            return blocks[random.Next(blocks.Length)];
        }

        public BlockShape GetAndUpdate() //return the next block and update the properties
        {
            BlockShape block = NextBlock;

            do
            {
                NextBlock = RandomBlock();
            }
            while (block.Id == NextBlock.Id);

            return block;
        }
    }
}
