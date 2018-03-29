using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class SongComponent : Component
    {
        private SongName song;
        private decimal duration;

        public SongName Song { get => song; }
        public decimal Duration { get => duration; }

        public SongComponent(Entity parent, SongName song, decimal duration = 30m) : base(parent)
        {
            this.song = song;
            this.duration = duration;
        }
    }
}
