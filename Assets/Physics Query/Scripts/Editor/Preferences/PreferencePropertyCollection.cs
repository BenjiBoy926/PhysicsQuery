using System.Collections.Generic;

namespace PQuery.Editor
{
    public class PreferencePropertyCollection<TValue>
    {
        private readonly TValue _defaultValue;
        private readonly List<PreferenceProperty<TValue>> _properties = new();
        
        public PreferencePropertyCollection(TValue defaultValue)
        {
            _defaultValue = defaultValue;
        }

        public void SetAllValues(TValue value)
        {
            for (int i = 0; i < _properties.Count; i++)
            {
                _properties[i].Value = value;
            }
        }
        public TValue GetValue(string name)
        {
            return GetProperty(name).Value;
        }
        public void SetValue(string name, TValue value)
        {
            GetProperty(name).Value = value;
        }
        private PreferenceProperty<TValue> GetProperty(string name)
        {
            PreferenceProperty<TValue> property = _properties.Find(x => x.Name == name);
            return property ?? AddNewProperty(name);
        }
        private PreferenceProperty<TValue> AddNewProperty(string name)
        {
            PreferenceProperty<TValue> newProperty = new(name, _defaultValue);
            _properties.Add(newProperty);
            return newProperty;
        }
    }
}