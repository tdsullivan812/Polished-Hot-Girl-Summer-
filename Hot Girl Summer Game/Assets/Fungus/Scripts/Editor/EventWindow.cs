// This code is part of the Fungus library (http://fungusgames.com) maintained by Chris Gregan (http://twitter.com/gofungus).
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

// Adapted from: https://github.com/thecodejunkie/unity.resources/blob/master/scripts/editor/ExtendedEditorWindow.cs

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Fungus.EditorUtils
{
	internal static class MouseButton
	{
		public const int Left = 0;
		public const int Right = 1;
		public const int Middle = 2;
	}

	public class EventWindow : EditorWindow
	{
		protected delegate void EventAction(UnityEngine.Event e);
		protected Dictionary<EventType, EventAction> eventTable;
		protected Dictionary<EventType, EventAction> rawEventTable;
		
		public EventWindow()
		{
			eventTable = new Dictionary<EventType, EventAction> {
				{ EventType.MouseDown,       e => OnMouseDown(e)       },
				{ EventType.MouseUp,         e => OnMouseUp(e)         },
				{ EventType.MouseDrag,       e => OnMouseDrag(e)       },
				{ EventType.MouseMove,       e => OnMouseMove(e)       },
				{ EventType.ScrollWheel,     e => OnScrollWheel(e)     },
				{ EventType.ContextClick,    e => OnContextClick(e)    },
				{ EventType.KeyDown,         e => OnKeyDown(e)         },
				{ EventType.KeyUp,           e => OnKeyUp(e)           },
				{ EventType.ValidateCommand, e => OnValidateCommand(e) },
				{ EventType.ExecuteCommand,  e => OnExecuteCommand(e)  },
			};
			rawEventTable = new Dictionary<EventType, EventAction> {
				{ EventType.MouseDown,       e => OnRawMouseDown(e)    },
				{ EventType.MouseUp,         e => OnRawMouseUp(e)      },
				{ EventType.MouseDrag,       e => OnRawMouseDrag(e)    },
				{ EventType.MouseMove,       e => OnRawMouseMove(e)    },				
			};
		}
		
		protected virtual void OnMouseDown(UnityEngine.Event e) { }
		protected virtual void OnMouseUp(UnityEngine.Event e)   { }
		protected virtual void OnMouseDrag(UnityEngine.Event e) { }
		protected virtual void OnMouseMove(UnityEngine.Event e) { }
		protected virtual void OnScrollWheel(UnityEngine.Event e) { }
		protected virtual void OnContextClick(UnityEngine.Event e) { }
		protected virtual void OnKeyDown(UnityEngine.Event e) { }
		protected virtual void OnKeyUp(UnityEngine.Event e) { }
		protected virtual void OnValidateCommand(UnityEngine.Event e) { }
		protected virtual void OnExecuteCommand(UnityEngine.Event e) { }

		protected virtual void OnRawMouseDown(UnityEngine.Event e) { }		
		protected virtual void OnRawMouseUp(UnityEngine.Event e)   { }
		protected virtual void OnRawMouseDrag(UnityEngine.Event e) { }
		protected virtual void OnRawMouseMove(UnityEngine.Event e) { }
		
		protected virtual void HandleEvents(UnityEngine.Event e)
		{
			EventAction handler;
			if (rawEventTable.TryGetValue(e.rawType, out handler))
			{
				handler.Invoke(e);
			}
			if (eventTable.TryGetValue(e.type, out handler))
			{
				handler.Invoke(e);
			}
		}
	}
}
