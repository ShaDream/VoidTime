﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VoidTime.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Data {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Data() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("VoidTime.Resources.Data", typeof(Data).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot; ?&gt;
        ///&lt;chips&gt;
        ///  &lt;chip&gt;
        ///    &lt;class&gt;system&lt;/class&gt;
        ///    &lt;value level=&quot;1&quot;&gt;1&lt;/value&gt;
        ///    &lt;name&gt;HP Gauge&lt;/name&gt;
        ///    &lt;description&gt;Display the player&apos;s HP gauge&lt;/description&gt;
        ///    &lt;size level=&quot;1&quot;&gt;
        ///      &lt;x&gt;1&lt;/x&gt;
        ///      &lt;y&gt;1&lt;/y&gt;
        ///    &lt;/size&gt;
        ///    &lt;levels&gt;1&lt;/levels&gt;
        ///  &lt;/chip&gt;
        ///
        ///  &lt;chip&gt;
        ///    &lt;class&gt;system&lt;/class&gt;
        ///    &lt;value level=&quot;1&quot;&gt;1&lt;/value&gt;
        ///    &lt;name&gt;System Chip&lt;/name&gt;
        ///    &lt;description&gt;Central system chip. removal means destroy&lt;/description&gt;
        ///    &lt;size level=&quot;1&quot;&gt;
        ///     [rest of string was truncated]&quot;;.
        /// </summary>
        public static string Chips {
            get {
                return ResourceManager.GetString("Chips", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot; ?&gt;
        ///
        ///&lt;ships&gt;
        ///
        ///  &lt;ship&gt;
        ///    &lt;name&gt;Ship&lt;/name&gt;
        ///    &lt;size&gt;
        ///      &lt;x&gt;&lt;/x&gt;
        ///      &lt;y&gt;&lt;/y&gt;
        ///    &lt;/size&gt;
        ///    &lt;guns&gt;
        ///      &lt;gunSlot&gt;
        ///        &lt;position&gt;
        ///          &lt;x&gt;&lt;/x&gt;
        ///          &lt;y&gt;&lt;/y&gt;
        ///        &lt;/position&gt;
        ///        &lt;maxTier&gt;&lt;/maxTier&gt;
        ///      &lt;/gunSlot&gt;
        ///    &lt;/guns&gt;
        ///    &lt;upgradeSize&gt;
        ///      &lt;x&gt;&lt;/x&gt;
        ///      &lt;y&gt;&lt;/y&gt;
        ///    &lt;/upgradeSize&gt;
        ///    &lt;defence&gt;&lt;/defence&gt;
        ///    &lt;hp&gt;&lt;/hp&gt;
        ///    &lt;speed&gt;&lt;/speed&gt;
        ///  &lt;/ship&gt;
        ///
        ///&lt;/ships&gt;.
        /// </summary>
        public static string Ships {
            get {
                return ResourceManager.GetString("Ships", resourceCulture);
            }
        }
    }
}