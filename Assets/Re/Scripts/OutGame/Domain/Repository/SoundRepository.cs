using System;
using Re.OutGame.Data.DataStore;
using UnityEngine;

namespace Re.OutGame.Domain.Repository
{
    public sealed class SoundRepository
    {
        private readonly BgmTable _bgmTable;
        private readonly SeTable _seTable;

        public SoundRepository(BgmTable bgmTable, SeTable seTable)
        {
            _bgmTable = bgmTable;
            _seTable = seTable;
        }

        public BgmData Find(BgmType type)
        {
            var data = _bgmTable.data.Find(x => x.type == type);
            if (data == null)
            {
                throw new Exception($"bgm data is null. (type: {type})");
            }

            return data;
        }

        public SeData Find(SeType type)
        {
            var data = _seTable.data.Find(x => x.type == type);
            if (data == null)
            {
                throw new Exception($"se data is null. (type: {type})");
            }

            return data;
        }
    }
}