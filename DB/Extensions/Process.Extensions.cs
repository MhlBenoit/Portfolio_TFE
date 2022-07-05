using System.ComponentModel;

//x <summary>
//! Process Extensions
//! <para>Author : Fabrice Mahieu</para>
//x </summary>
public static partial class ProcessExtensions
{
    //x <summary>
    //! Any method for which use "invoke" mechanism
    //! <para>Usefull for inter-thread call</para>
    //x </summary>
    //! <typeparam name="T">Type of object on which call "invoke" method</typeparam>
    //! <param name="Object">Object on which call "invoke" method</param>
    public delegate void InvokeIfRequiredMethod<T>(T Object)
        where T : ISynchronizeInvoke;

    //x <summary>
    //! Execute the specified method using "invoke" mechanism if it is required
    //! <para>Usefull for inter-thread call</para>
    //x </summary>
    //! <typeparam name="T">Type of object on which call "invoke" method</typeparam>
    //! <param name="Object">Object on which call "invoke" method</param>
    //! <param name="Action">Method to execute</param>
    public static void InvokeIfRequired<T>(this T Object, InvokeIfRequiredMethod<T> Action)
        where T : ISynchronizeInvoke
    {
        if (Object == null) return;
        if (Object.InvokeRequired)
        {
            Object.Invoke(Action, new object[] { Object });
        }
        else
        {
            Action(Object);
        }
    }
	
	/*
	// Sample of inter-thread call
	{
		// Any form class
		... class MyForm : Form
		{
			.
			.
			.
			
			//! Any control member
			..... MyControl
			
			//! Any method (non static)
			....(.....)
			{
				
				var myTask = new System.Threading.Thread(new System.Threading.ThreadStart(() =>
				{
					.
					.
					.
					
					this.InvokeIfRequired(() =>
					{
						MyControl.anyProperty = anyValue;
						MyControl.anyMethod(...);
					});
					
					.
					.
					.
				});
				myTask.Start();
				
				
			}
			
			.
			.
			.
		}
	}
	*/
}
