using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoterX.Core.Elections;
using VoterX.Utilities.Models;

namespace VoterX.Utilities.Extensions
{
    public static class WizardSelectorExtensions
    {
        // Swtich logic for converting a generic list object
        public static List<WizardSelectorItem> ToWizardSelectors<T>(this List<T> listData)
        {
            if (typeof(T) == typeof(LogCodeModel))
            {
                return (listData as List<LogCodeModel>).ToWizardSelectors();
            }
            else if (typeof(T) == typeof(LocationModel))
            {
                return (listData as List<LocationModel>).ToWizardSelectors();
            }
            else if (typeof(T) == typeof(JurisdictionModel))
            {
                return (listData as List<JurisdictionModel>).ToWizardSelectors();
            }
            else if (typeof(T) == typeof(BallotStyleModel))
            {
                return (listData as List<BallotStyleModel>).ToWizardSelectors();
            }
            if (typeof(T) == typeof(PartyModel))
            {
                return (listData as List<PartyModel>).ToWizardSelectors();
            }
            else
            {
                return null;
            }
        }

        public static List<WizardSelectorItem> ToWizardSelectors(this List<LogCodeModel> codes)
        {
            return codes.Select(c => new WizardSelectorItem
            {
                Name = c.LogDescription.ToUpper(),
                Value = c.LogCode.ToString()
            }).ToList();
        }

        public static List<WizardSelectorItem> ToWizardSelectors(this List<LocationModel> locations)
        {
            return locations.Select(l => new WizardSelectorItem
            {
                Name = l.PlaceName.ToUpper(),
                Value = l.PollId.ToString()
            }).ToList();
        }

        public static List<WizardSelectorItem> ToWizardSelectors(this List<JurisdictionModel> jurisdictions)
        {
            return jurisdictions.Select(j => new WizardSelectorItem
            {
                Name = j.JurisdictionName.ToUpper(),
                Value = j.JurisdictionId.ToString(),
                Category = j.JurisdictionType
            }).ToList();
        }

        public static List<WizardSelectorItem> ToWizardSelectors(this List<BallotStyleModel> styles)
        {
            return styles.Select(s => new WizardSelectorItem
            {
                Name = s.BallotStyleName.ToUpper(),
                Value = s.BallotStyleId.ToString()
            }).ToList();
        }

        public static List<WizardSelectorItem> ToWizardSelectors(this List<PartyModel> parties)
        {
            return parties.Select(p => new WizardSelectorItem
            {
                Name = p.PartyCode.ToUpper(),
                Value = p.PartyCode.ToString()
            }).ToList();
        }
    }
}
