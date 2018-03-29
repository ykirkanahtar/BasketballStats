﻿using BasketballStats.WebApi.Data;
using Newtonsoft.Json;

namespace BasketballStats.WebApi.Models
{
    public class Stat : BaseModel<int>
    {
        public int MatchId { get; set; }
        public int TeamId { get; set; }
        public int PlayerId { get; set; }

        public decimal OnePoint { get; set; }
        public decimal TwoPoint { get; set; }
        public decimal MissingOnePoint { get; set; }
        public decimal MissingTwoPoint { get; set; }
        public decimal Rebound { get; set; }
        public decimal StealBall { get; set; }
        public decimal LooseBall { get; set; }
        public decimal Assist { get; set; }
        public decimal Interrupt { get; set; }

        [JsonIgnore]
        public Match Match { get; set; }
        [JsonIgnore]
        public Team Team { get; set; }
        [JsonIgnore]
        public Player Player { get; set; }

    }
}
