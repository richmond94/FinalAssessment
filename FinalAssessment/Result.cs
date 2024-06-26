﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FinalAssessment
{
    public class Result
    {
        public int placed { get; private set; }
        public double raceTime { get; private set; }
        public bool qualified { get;  set; }

        public Result(int placed, double raceTime, bool qualified)
        {
            this.placed = placed;
            this.raceTime = raceTime;
            this.qualified = qualified;
        }

        public bool isQualified()
        {
            return placed <= 3;
        }

        public override string ToString()
        {
            return $"Placed: {placed}, Race Time: {raceTime}s, Qualified: {(qualified ? "Yes" : "No")}";
        }

        public string ToFile()
        {
            return $"{placed},{raceTime},{qualified}";
        }
    }
}
