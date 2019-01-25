// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Android.App;
using Android.Runtime;
using Microsoft.Intune.Mam.Client.App;
using Microsoft.Intune.Mam.Client.Notification;
using Microsoft.Intune.Mam.Policy.Notification;

namespace IntuneRepro
{
    [Application]
    class App : MAMApplication
    {
        public App(IntPtr handle, JniHandleOwnership transfer)
            : base(handle, transfer) { }

        public override void OnMAMCreate()
        {
            // Register the notification receivers
            IMAMNotificationReceiverRegistry registry = MAMComponents.Get<IMAMNotificationReceiverRegistry>();
            registry.RegisterReceiver(new MAMNotificationReceiver(), MAMNotificationType.MamEnrollmentResult);

            base.OnMAMCreate();
        }
    }

    class MAMNotificationReceiver : Java.Lang.Object, IMAMNotificationReceiver
    {
        public bool OnReceive(IMAMNotification notification)
        {
            Android.Util.Log.Debug("IntuneRepro", $"Notification: {notification.Type}");
            return true;
        }
    }
}