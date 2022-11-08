using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ILabelBL
    {
        public LabelEntity AddLabel(long userId, string name);
        public LabelEntity EditLabel(long userId, long labelId, string name);
        public bool DeleteLabel(long userId, long labelId);
        public IEnumerable<LabelEntity> ViewLabel(long userId);
    }
}
