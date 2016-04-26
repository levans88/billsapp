using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace billsapp {
    public static class StringExtensions {

        // Truncate string to specific length
        public static string Truncate(this string value, int maxLength) {
            if (string.IsNullOrEmpty(value)) {
                return value;
            }
            else {
                return value.Length <= maxLength ? value : value.Substring(0, maxLength);
            }
        }

        // Truncate or add spaces to string to make it a specific length
        public static string FixLength(this string value, int maxLength) {
            
            // Pad
            if (value.Length < maxLength) {
                var lengthDifference = maxLength - value.Length;
                for (int i = 0; i <= lengthDifference; i++) {
                    value += "&nbsp;";
                }
                return value;
            }
            // Truncate
            else if (value.Length > maxLength) {
                return value.Substring(0, maxLength - 3) + "...";
            }
            else { 
                return value;
            }
        }
    }
}