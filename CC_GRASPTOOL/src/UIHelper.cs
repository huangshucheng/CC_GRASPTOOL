using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows;
using System.Windows.Threading;
using System.ComponentModel;

namespace CC_GRASPTOOL
{
    class UIHelper: Application
    {
         private static DispatcherOperationCallback exitFrameCallback = new DispatcherOperationCallback(ExitFrame);
         public static void DoEvents()
         {
             DispatcherFrame nestedFrame = new DispatcherFrame();
             DispatcherOperation exitOperation = Dispatcher.CurrentDispatcher.BeginInvoke( DispatcherPriority.Background, exitFrameCallback, nestedFrame);
             Dispatcher.PushFrame(nestedFrame);
             if (exitOperation.Status != DispatcherOperationStatus.Completed)
             {
                 exitOperation.Abort();
            }
         }
         private static Object ExitFrame(Object state)
         {
             DispatcherFrame frame = state as DispatcherFrame;
             if (frame != null)
             {
                 frame.Continue = false;
             }
             return null;
        }
        /*
         public void DoEvents()
         {
             DispatcherFrame frame = new DispatcherFrame();
             Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new DispatcherOperationCallback(ExitFrames), frame);
             Dispatcher.PushFrame(frame);
         }
         public object ExitFrames(object f)
         {
             ((DispatcherFrame)f).Continue = false;
             return null;
         }
        */
    }
}
