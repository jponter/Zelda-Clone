﻿//  DeserializeSettings


namespace Leguar.TotalJSON {
	
	/// <summary>
	/// Settings for deserialization.
	/// </summary>
	public class DeserializeSettings {

		private bool allowNonStringDictionaryKeys = false;

		/// <summary>
		/// By default this is false. Meaning that if any JSON object is deserialized to dictionary, target dictionary must be using string keys, like JSON itself is using.
		/// 
		/// If this is set false, dictionaries key type may also be integer or long. In this case deserialization try to change JSON keys to required dictionary key type.
		/// Note however that this may cause several problems when deserializing. For example, JSON object may contain keys "1" and "01". Deserializing this to Dictionary
		/// with string keys can be done without issues. But deserializing that JSON to Dictionary with integer keys causes error due duplicate key, even original JSON is
		/// completely valid.
		/// </summary>
		/// <value>
		/// False by default, set true to allow more loose and flexible deserialization to dictionaries.
		/// </value>
		public bool AllowNonStringDictionaryKeys {
			set {
				allowNonStringDictionaryKeys = value;
			}
			get {
				return allowNonStringDictionaryKeys;
			}
		}

		private bool requireAllFieldsArePopulated = true;

		/// <summary>
		/// Default is true, meaning all the public fields in class/struct where JSON is deserialized must get their value set. So source JSON must contain matching values
		/// for all the fields.
		/// 
		/// If set to false, fields that have no matching data in JSON are just left in their default values.
		/// 
		/// For example, with default setting, deserializing JSON {"a":1,"b":2} to class { public int a; public int b; public int c; } will cause exception since there's
		/// no value for field 'c'.
		/// </summary>
		/// <value>
		/// True by default, set false to allow classes/structs not to get fully populated.
		/// </value>
		public bool RequireAllFieldsArePopulated {
			set {
				requireAllFieldsArePopulated = value;
			}
			get {
				return requireAllFieldsArePopulated;
			}
		}

		private bool requireAllJSONValuesAreUsed = false;

		/// <summary>
		/// Default is false, meaning that any possible extra values in source JSON are ignored. If set to true, it is strictly required that all the values from JSON must
		/// get used to populate some field in target class/struct.
		/// 
		/// For example, with default setting, deserializing JSON {"a":1,"b":2,"c":3} to class { public int a; public int b; } is acceptable and JSON value for 'c' is just
		/// not used.
		/// </summary>
		/// <value>
		/// False by default, set true to require that everything in source JSON is used.
		/// </value>
		public bool RequireAllJSONValuesAreUsed {
			set {
				requireAllJSONValuesAreUsed = value;
			}
			get {
				return requireAllJSONValuesAreUsed;
			}
		}

	}

}
