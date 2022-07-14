using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoterX.Core.Elections;
using VoterX.Utilities.Models;

namespace VoterX.Utilities.Factories
{
    public class WizardSelectorFactory
    {
        public WizardSelectorFactory()
        {

        }

        #region CreationMethods
        // Swtich logic for converting a generic list object
        public List<WizardSelectorItem> Create<T>(List<T> listData)
        {
            if(typeof(T) == typeof(LogCodeModel))
            {
                return Create((listData as List<LogCodeModel>));
            }
            else if (typeof(T) == typeof(LocationModel))
            {
                return Create((listData as List<LocationModel>));
            }
            else if (typeof(T) == typeof(JurisdictionModel))
            {
                return Create((listData as List<JurisdictionModel>));
            }
            else if (typeof(T) == typeof(BallotStyleModel))
            {
                return Create((listData as List<BallotStyleModel>));
            }
            if (typeof(T) == typeof(PartyModel))
            {
                return Create((listData as List<PartyModel>));
            }
            else
            {
                return null;
            }
        }

        public List<WizardSelectorItem> Create(List<LogCodeModel> codes)
        {
            // Check for empty list
            if(codes != null && codes.Count() > 0)
            {
                // Create new list
                List<WizardSelectorItem> itemList = new List<WizardSelectorItem>();

                // Loop through list
                foreach(var item in codes)
                {
                    // Add new item to list
                    itemList.Add(new WizardSelectorItem
                    {
                        Name = item.LogDescription.ToUpper(),
                        Value = item.LogCode.ToString()
                    });
                }

                return itemList;
            }
            else
            {
                // Create empty list
                return new List<WizardSelectorItem>();
            }
        }

        public List<WizardSelectorItem> Create(List<LocationModel> locations)
        {
            // Check for empty list
            if (locations != null && locations.Count() > 0)
            {
                // Create new list
                List<WizardSelectorItem> itemList = new List<WizardSelectorItem>();

                // Loop through list
                foreach (var item in locations)
                {
                    // Add new item to list
                    itemList.Add(new WizardSelectorItem
                    {
                        Name = item.PlaceName.ToUpper(),
                        Value = item.PollId.ToString()
                    });
                }

                return itemList;
            }
            else
            {
                // Create empty list
                return new List<WizardSelectorItem>();
            }
        }

        public List<WizardSelectorItem> Create(List<JurisdictionModel> jurisdictions)
        {
            // Check for empty list
            if (jurisdictions != null && jurisdictions.Count() > 0)
            {
                // Create new list
                List<WizardSelectorItem> itemList = new List<WizardSelectorItem>();

                // Loop through list
                foreach (var item in jurisdictions)
                {
                    // Add new item to list
                    itemList.Add(new WizardSelectorItem
                    {
                        Name = item.JurisdictionName.ToUpper(),
                        Value = item.JurisdictionId.ToString(),
                        Category = item.JurisdictionType
                    });
                }

                return itemList;
            }
            else
            {
                // Create empty list
                return new List<WizardSelectorItem>();
            }
        }

        public List<WizardSelectorItem> Create(List<BallotStyleModel> styles)
        {
            // Check for empty list
            if (styles != null && styles.Count() > 0)
            {
                // Create new list
                List<WizardSelectorItem> itemList = new List<WizardSelectorItem>();

                // Loop through list
                foreach (var item in styles)
                {
                    // Add new item to list
                    itemList.Add(new WizardSelectorItem
                    {
                        Name = item.BallotStyleName.ToUpper(),
                        Value = item.BallotStyleId.ToString()
                    });
                }

                return itemList;
            }
            else
            {
                // Create empty list
                return new List<WizardSelectorItem>();
            }
        }

        public List<WizardSelectorItem> Create(List<PartyModel> parties)
        {
            // Check for empty list
            if (parties != null && parties.Count() > 0)
            {
                // Create new list
                List<WizardSelectorItem> itemList = new List<WizardSelectorItem>();

                // Loop through list
                foreach (var item in parties)
                {
                    // Add new item to list
                    itemList.Add(new WizardSelectorItem
                    {
                        Name = item.PartyCode.ToUpper(),
                        Value = item.PartyCode
                    });
                }

                return itemList;
            }
            else
            {
                // Create empty list
                return new List<WizardSelectorItem>();
            }
        }
        #endregion
    }

    #region ConvertBackMethods
    // Still need to pass the original list in order to find the correct item
    public static class WizardSelectConverter
    {        
        public static LogCodeModel Convert(this WizardSelectorItem item, List<LogCodeModel> codes)
        {
            return codes.Where(lc => lc.LogCode.ToString() == item.Value).FirstOrDefault();
        }

        public static LocationModel Convert(this WizardSelectorItem item, List<LocationModel> locations)
        {
            return locations.Where(l => l.PollId.ToString() == item.Value).FirstOrDefault();
        }

        public static JurisdictionModel Convert(this WizardSelectorItem item, List<JurisdictionModel> jurisdictions)
        {
            return jurisdictions.Where(j => j.JurisdictionId.ToString() == item.Value).FirstOrDefault();
        }

        public static BallotStyleModel Convert(this WizardSelectorItem item, List<BallotStyleModel> styles)
        {
            return styles.Where(l => l.BallotStyleId.ToString() == item.Value).FirstOrDefault();
        }

        public static PartyModel Convert(this WizardSelectorItem item, List<PartyModel> parties)
        {
            return parties.Where(l => l.PartyCode == item.Value).FirstOrDefault();
        }
    }
    #endregion
}
