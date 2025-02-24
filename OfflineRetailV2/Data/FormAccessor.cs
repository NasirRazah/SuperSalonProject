using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
namespace OfflineRetailV2.Data
{
    public class FormAccessor
    {
        private static FormAccessor.ThreadSafeObjectProvider<FormAccessor> m_MyFormsObjectProvider = new ThreadSafeObjectProvider<FormAccessor>();
        public MessageBoxLoadingWindow _MsgLoadingBox;
        public MessageBoxLoadingWindow MsgLoadingBox
        {
            get
            {
                this._MsgLoadingBox = FormAccessor.Create__Instance__<MessageBoxLoadingWindow>(this._MsgLoadingBox);
                return this._MsgLoadingBox;
            }
            set
            {
                if (value != this._MsgLoadingBox)
                {
                    if (value != null)
                    {
                        throw new ArgumentException("Property can only be set to Nothing");
                    }
                    this.Dispose__Instance__<MessageBoxLoadingWindow>(ref this._MsgLoadingBox);
                }
            }
        }


        private static Hashtable m_FormBeingCreated;
        private static T Create__Instance__<T>(T Instance)
            where T : Window, new()
        {
            T instance;
            //   if ((Instance == null || Instance.IsDisposed ? 0 : 1) == 0)
            if (Instance == null)
            {
                if (FormAccessor.m_FormBeingCreated == null)
                {
                    FormAccessor.m_FormBeingCreated = new Hashtable();
                }
                else if (FormAccessor.m_FormBeingCreated.ContainsKey(typeof(T)))
                {
                    // throw new InvalidOperationException(Utils.GetResourceString("WinForms_RecursiveFormCreate", new string[0]));
                }
                FormAccessor.m_FormBeingCreated.Add(typeof(T), null);
                try
                {
                    instance = Activator.CreateInstance<T>();
                }
                finally
                {
                    FormAccessor.m_FormBeingCreated.Remove(typeof(T));
                }
            }
            else
            {
                instance = Instance;
            }
            return instance;
        }

        private void Dispose__Instance__<T>(ref T instance)
        where T : Window
        {
            //   instance.Dispose();
            instance = default(T);
        }


        internal static FormAccessor Forms
        {
            get
            {
                return FormAccessor.m_MyFormsObjectProvider.GetInstance;
            }
        }



        internal sealed class ThreadSafeObjectProvider<T>
    where T : new()
        {

            private static T m_ThreadStaticValue;

            internal T GetInstance
            {
                get
                {
                    if (FormAccessor.ThreadSafeObjectProvider<T>.m_ThreadStaticValue == null)
                    {
                        FormAccessor.ThreadSafeObjectProvider<T>.m_ThreadStaticValue = Activator.CreateInstance<T>();
                    }
                    return FormAccessor.ThreadSafeObjectProvider<T>.m_ThreadStaticValue;
                }
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            public ThreadSafeObjectProvider()
            {
            }
        }
    }

}
