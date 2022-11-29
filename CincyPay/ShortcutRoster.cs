using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortCutspace

{
    public static class ShortcutRoster
    {
        static ShortcutRoster() {
            allShortcuts = new List<Shortcut>();
        }

        public static IList<Shortcut> allShortcuts { get; set; }

    }
}
