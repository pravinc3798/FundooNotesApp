using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace RepositoryLayer.Entity
{
    public class NoteLabelEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NLID { get; set; }
        [ForeignKey("Label")]
        public long LabelId { get; set; }
        [ForeignKey("Note")]
        public long NoteId { get; set; }

        [JsonIgnore]
        public virtual LabelEntity Label { get; set; }
        [JsonIgnore]
        public virtual NoteEntity Note { get; set; }
    }
}
