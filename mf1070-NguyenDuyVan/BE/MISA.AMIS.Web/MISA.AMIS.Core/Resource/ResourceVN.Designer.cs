﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MISA.AMIS.Core.Resource {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ResourceVN {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ResourceVN() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MISA.AMIS.Core.Resource.ResourceVN", typeof(ResourceVN).Assembly);
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
        ///   Looks up a localized string similar to Mã đơn vị không được phép để trống.
        /// </summary>
        public static string Error_DepartmentCodeEmpty {
            get {
                return ResourceManager.GetString("Error_DepartmentCodeEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Mã đơn vị đã tồn tại vui lòng kiểm tra lại.
        /// </summary>
        public static string Error_DepartmentCodeExits {
            get {
                return ResourceManager.GetString("Error_DepartmentCodeExits", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tên đơn vị không được phép để trống.
        /// </summary>
        public static string Error_DepartmentNameEmpty {
            get {
                return ResourceManager.GetString("Error_DepartmentNameEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email không đúng định dạng.
        /// </summary>
        public static string Error_Email {
            get {
                return ResourceManager.GetString("Error_Email", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Mã nhân viên không được phép để trống.
        /// </summary>
        public static string Error_EmployeeCodeEmpty {
            get {
                return ResourceManager.GetString("Error_EmployeeCodeEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Mã nhân viên đã tồn tại trong hệ thống, vui lòng kiểm tra lại..
        /// </summary>
        public static string Error_EmployeeCodeExits {
            get {
                return ResourceManager.GetString("Error_EmployeeCodeExits", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tên nhân viên không được phép để trống.
        /// </summary>
        public static string Error_EmployeeNameEmpty {
            get {
                return ResourceManager.GetString("Error_EmployeeNameEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Có lỗi xảy ra vui lòng liên hệ MISA để được trợ giúp.
        /// </summary>
        public static string Error_Exception {
            get {
                return ResourceManager.GetString("Error_Exception", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Danh sách nhân viên.
        /// </summary>
        public static string ExelSheetTitle {
            get {
                return ResourceManager.GetString("ExelSheetTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nữ.
        /// </summary>
        public static string Female {
            get {
                return ResourceManager.GetString("Female", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nam.
        /// </summary>
        public static string Male {
            get {
                return ResourceManager.GetString("Male", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Khác.
        /// </summary>
        public static string Other {
            get {
                return ResourceManager.GetString("Other", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Thông tin {0} không được phép để trống.
        /// </summary>
        public static string Validate_Empty {
            get {
                return ResourceManager.GetString("Validate_Empty", resourceCulture);
            }
        }
    }
}