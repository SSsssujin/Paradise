using System.Collections.Generic;
using System.Text;
using Paradise.Data.Unit;
using UnityEngine;

namespace Paradise
{
    public class Party
    {
        private List<BasicUnitData> _basicUnitList = new();
        private List<EliteUnitData> _eliteUnitList = new();

        public bool Add(UnitData data)
        {
            // Basic
            if (data is BasicUnitData basicUnitData)
            {
                if (MaxCount.Basic >= _basicUnitList.Count)
                {
                    return false;
                }
                _basicUnitList.Add(basicUnitData);
            }
            // Elite
            if (data is EliteUnitData eliteUnitData)
            {
                if (MaxCount.Elite >= _eliteUnitList.Count)
                {
                    return false;
                }
                _eliteUnitList.Add(eliteUnitData);
            }
            return true;
        }

        public IEnumerable<UnitData> GetUnitsByType(UnitType type)
        {
            return type switch
            {
                UnitType.Basic => _basicUnitList,
                UnitType.Elite => _eliteUnitList,
                _ => new List<UnitData>()
            };
        }

        public void Print()
        {
            StringBuilder sb = new();
            sb.AppendLine($"===== Basic : [{_basicUnitList.Count}] =====");
            foreach (var unit in _basicUnitList)
            {
                sb.AppendLine(unit.Name);
            }
            sb.AppendLine();
            sb.AppendLine($"===== Elite : [{_eliteUnitList.Count}] =====");
            foreach (var unit in _eliteUnitList)
            {
                sb.AppendLine(unit.Name);
            }
            Debug.Log(sb.ToString());
        }

        public List<BasicUnitData> BasicUnitList => _basicUnitList;
        public List<EliteUnitData> EliteUnitList => _eliteUnitList;
    }
}