using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;
using RestSharp;
using APIClasses;
using System.IO;
using System.Windows.Controls.Primitives;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.Runtime.Remoting.Channels;

namespace WPFClient
{
	//public delegate IRestResponse SearchDel(RestRequest request);
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	/// 
	/*Class behind the WPF Client, used to call and prepare for the business tier*/
	public partial class MainWindow : Window
	{

		private string URL;
		private RestClient client;
		public MainWindow()
		{
			InitializeComponent();
			ProgressBar.Visibility = Visibility.Hidden;
			URL = "https://localhost:44386/";
			client = new RestClient(URL);
			//Requesting the values
			RestRequest request = new RestRequest("api/values");
			IRestResponse numOfThings = client.Get(request);


			//Also tell me how many entries are in the DB and display them
			LoadingText.Visibility = Visibility.Hidden;
			string str = numOfThings.Content;
			IndexBox.Text = str;
		}


		/* Changes all the UI elements to visible or hidden 
		 * and changes whether the are read only depending on 
		 * what is passed in
		 */ 
		private void ChangeUIElements(bool toChange)
		{

			FNameBox.IsReadOnly = toChange;
			LNameBox.IsReadOnly = toChange;
			BalanceBox.IsReadOnly = toChange;
			AcctNoBox.IsReadOnly = toChange;
			PinBox.IsReadOnly = toChange;
			IndexBox.IsReadOnly = toChange;
			GoButt.IsEnabled = !toChange;
			SearchButt.IsEnabled = !toChange;

			if (toChange)
			{
				FNameBox.Visibility = Visibility.Hidden;
				LNameBox.Visibility = Visibility.Hidden;
				BalanceBox.Visibility = Visibility.Hidden;
				AcctNoBox.Visibility = Visibility.Hidden;
				PinBox.Visibility = Visibility.Hidden;
				IndexBox.Visibility = Visibility.Hidden;
				GoButt.Visibility = Visibility.Hidden;
				SearchButt.Visibility = Visibility.Hidden;

				Photo.Visibility = Visibility.Hidden;
				LoadingText.Visibility = Visibility.Visible;

				EditFnameButt.Visibility = Visibility.Hidden;
				EditLnameButt.Visibility = Visibility.Hidden;
				EditAccButt.Visibility = Visibility.Hidden;
				EditBalanceButt.Visibility = Visibility.Hidden;
				EditPinButt.Visibility = Visibility.Hidden;
				EditPhotoButt.Visibility = Visibility.Hidden;
			}
			else
			{
				FNameBox.Visibility = Visibility.Visible;
				LNameBox.Visibility = Visibility.Visible;
				BalanceBox.Visibility = Visibility.Visible;
				AcctNoBox.Visibility = Visibility.Visible;
				PinBox.Visibility = Visibility.Visible;
				IndexBox.Visibility = Visibility.Visible;
				GoButt.Visibility = Visibility.Visible;
				SearchButt.Visibility = Visibility.Visible;
				Photo.Visibility = Visibility.Visible;
				LoadingText.Visibility = Visibility.Hidden;

				EditFnameButt.Visibility = Visibility.Visible;
				EditLnameButt.Visibility = Visibility.Visible;
				EditAccButt.Visibility = Visibility.Visible;
				EditBalanceButt.Visibility = Visibility.Visible;
				EditPinButt.Visibility = Visibility.Visible;
				EditPhotoButt.Visibility = Visibility.Visible;
			}
			
		}


		/* Allows for the user to select a new random
		 * photo and calls the async method. This may 
		 * not always produce a new avatar everytime
		 * if i had more time I would have fixed this by
		 * tracking which photo the user has and not 
		 * including that in the randomiser
		 */ 
		private void EditPhoto(object sender, RoutedEventArgs e)
		{
			SearchData newAcct = new APIClasses.SearchData();
			newAcct.index = Int32.Parse(IndexBox.Text);
			Task search = EditPhotoAsync(sender, e, newAcct);
		}

		/* Called to randomise the photo asynchronously*/
		private async Task EditPhotoAsync(object sender, RoutedEventArgs e, SearchData newPhoto)
		{
			ProgressBar.Visibility = Visibility.Visible;
			ChangeUIElements(true);//Makes most boxes readonly
			await Task.Run(() =>
			{
				//sending request
				RestRequest request = new RestRequest("api/editphoto");
				request.AddJsonBody(newPhoto);
				//Do the request
				IRestResponse resp = client.Post(request);
				this.Dispatcher.Invoke(() =>
				{

					//Return ui back to normal
					ChangeUIElements(false);
					ProgressBar.Visibility = Visibility.Hidden;
					SearchBox.Visibility = Visibility.Visible;
					SearchIndex(sender, e);
				});
			});
		}

		/* Allows for the user to enter a new pin
		 * this has no limit on its size to increase
		 * security
		 */ 
		private void EditPin(object sender, RoutedEventArgs e)
		{
			//Using the searchdata class to bundle data
			SearchData newAcct = new APIClasses.SearchData();
			newAcct.searchStr = PinBox.Text;
			newAcct.index = Int32.Parse(IndexBox.Text);
			Task search = EditPinAsync(sender, e, newAcct);
		}

		/* Asynchronously enters the new pin*/ 
		private async Task EditPinAsync(object sender, RoutedEventArgs e, SearchData newAcct)
		{
			ProgressBar.Visibility = Visibility.Visible;
			ChangeUIElements(true);
			await Task.Run(() =>
			{
				RestRequest request = new RestRequest("api/editpin");
				request.AddJsonBody(newAcct);
				//Do the request
				IRestResponse resp = client.Post(request);
				this.Dispatcher.Invoke(() =>
				{
					if (resp.IsSuccessful)//If its successful then update everything
					{

						ChangeUIElements(false);
						ProgressBar.Visibility = Visibility.Hidden;
					}
					else //If the response isn't successful
					{
						string errmsg = resp.Content;
						MessageBox.Show(errmsg);//pop up with error message

					}
					//Return ui back to normal
					ChangeUIElements(false);
					ProgressBar.Visibility = Visibility.Hidden;
					SearchBox.Visibility = Visibility.Visible;
				});
			});
		}

		/* Allows the user to edit their account number
		 * takes in their new account number and
		 * their index
		 */
		private void EditAccount(object sender, RoutedEventArgs e)
		{
			SearchData newAcct = new APIClasses.SearchData();
			newAcct.searchStr = AcctNoBox.Text;
			newAcct.index = Int32.Parse(IndexBox.Text);
			Task search = EditAccountAsync(sender, e, newAcct);
		}

		/*Asynchronously changes the account number*/
		private async Task EditAccountAsync(object sender, RoutedEventArgs e, SearchData newAcct)
		{
			ProgressBar.Visibility = Visibility.Visible;//shows its loading
			ChangeUIElements(true);
			await Task.Run(() =>
			{
				RestRequest request = new RestRequest("api/editacct");
				request.AddJsonBody(newAcct);
				//Do the request
				IRestResponse resp = client.Post(request);
				this.Dispatcher.Invoke(() =>
				{
					if (resp.IsSuccessful)//If the response is successful
					{

						ChangeUIElements(false);//Change from readonly
						ProgressBar.Visibility = Visibility.Hidden;
					}
					else //If the response isn't successful
					{
						string errmsg = resp.Content;
						MessageBox.Show(errmsg);

					}
					//Return ui back to normal
					ChangeUIElements(false);
					ProgressBar.Visibility = Visibility.Hidden;
					SearchBox.Visibility = Visibility.Visible;
				});
			});
		}

		/* Allows the user to edit their balance
		 * takes in their new balance and their index
		 */
		private void EditBalance(object sender, RoutedEventArgs e)
		{
			SearchData newBalance = new APIClasses.SearchData();
			newBalance.searchStr = BalanceBox.Text;
			newBalance.index = Int32.Parse(IndexBox.Text);
			Task search = EditBalanceAsync(sender, e, newBalance);
		}

		/*Asynchronously Edits the balance of the user at that index*/
		private async Task EditBalanceAsync(object sender, RoutedEventArgs e, SearchData newBalance)
		{
			ProgressBar.Visibility = Visibility.Visible;
			ChangeUIElements(true);
			await Task.Run(() =>
			{
				RestRequest request = new RestRequest("api/editbalance");
				request.AddJsonBody(newBalance);
				//Do the request
				IRestResponse resp = client.Post(request);
				this.Dispatcher.Invoke(() =>
				{
					if (resp.IsSuccessful)
					{

						ChangeUIElements(false);
						ProgressBar.Visibility = Visibility.Hidden;
					}
					else //If the response isn't successful
					{
						string errmsg = resp.Content;
						MessageBox.Show(errmsg);
						
					}
					//Return ui back to normal
					ChangeUIElements(false);
					ProgressBar.Visibility = Visibility.Hidden;
					SearchBox.Visibility = Visibility.Visible;
				});
			});
		}

		/* Allows the user to change their last name
		 * Little error handling because a name can be anything
		 */ 
		private void EditLName(object sender, RoutedEventArgs e)
		{
			SearchData newName = new APIClasses.SearchData();
			//Converted to upper because thats how last names are stored in the database
			newName.searchStr = LNameBox.Text.ToUpper();
			newName.index = Int32.Parse(IndexBox.Text);
			Task search = EditLNameAsync(sender, e, newName);
		}

		/* Asynchronously changes the last name*/ 
		private async Task EditLNameAsync(object sender, RoutedEventArgs e, SearchData newName)
		{
			ProgressBar.Visibility = Visibility.Visible;
			ChangeUIElements(true);
			await Task.Run(() =>
			{
				RestRequest request = new RestRequest("api/editlname");
				request.AddJsonBody(newName);
				//Do the request
				IRestResponse resp = client.Post(request);
				this.Dispatcher.Invoke(() =>
				{
					//Return ui back to normal
					ChangeUIElements(false);
					ProgressBar.Visibility = Visibility.Hidden;
					SearchBox.Visibility = Visibility.Visible;
				});
			});
		}

		/* Allows the user to change the users first name
		 * Minimal error handling because it can be anything
		 */
		private void EditFName(object sender, RoutedEventArgs e)
		{
			//do nothing atm
			SearchData newName = new APIClasses.SearchData();
			newName.searchStr = FNameBox.Text;
			newName.index = Int32.Parse(IndexBox.Text);
			Task search = EditFNameAsync(sender, e, newName);

		}
		/*Asynchronously changing that specific users first name*/
		private async Task EditFNameAsync(object sender, RoutedEventArgs e, SearchData newName)
		{

			ProgressBar.Visibility = Visibility.Visible;
			ChangeUIElements(true);
			await Task.Run(() =>
			{
				RestRequest request = new RestRequest("api/editfname");
				request.AddJsonBody(newName);
				//Do the request
				IRestResponse resp = client.Post(request);
				this.Dispatcher.Invoke(() =>
				{
					//Return ui back to normal
					ChangeUIElements(false);
					ProgressBar.Visibility = Visibility.Hidden;
					SearchBox.Visibility = Visibility.Visible;
				});
			});
		}

		/* Allows the user to search for the first user
		 * with a certain last name
		 */ 
		private void SearchName(object sender, RoutedEventArgs e)
		{
			IndexBox.Text = "";//Removing the index

			Task searching = SearchNameAsync(sender, e);
			ProgressBar.Visibility = Visibility.Visible;
			ChangeUIElements(true);
		}

		/*Asynchronously searching for someone with that last name*/
		private async Task SearchNameAsync(object sender, RoutedEventArgs e)
		{
			SearchData mySearch = new APIClasses.SearchData();
			mySearch.searchStr = SearchBox.Text.ToUpper();

			ProgressBar.Visibility = Visibility.Visible;
			ChangeUIElements(true);
			await Task.Run(() =>
			{

				RestRequest request = new RestRequest("api/search");
				request.AddJsonBody(mySearch);
				//Do the request
				IRestResponse resp = client.Post(request);
				this.Dispatcher.Invoke(() =>
				{
					if (resp.IsSuccessful)//IF that name exists in the database
					{
						TransitionModel transModel = JsonConvert.DeserializeObject<TransitionModel>(resp.Content);
						DataIntermed dataIntermed = JsonConvert.DeserializeObject<DataIntermed>(transModel.message);

						/*Updating the users fields*/
						FNameBox.Text = dataIntermed.fName;
						LNameBox.Text = dataIntermed.lName;
						BalanceBox.Text = dataIntermed.bal.ToString("C");//"C" so its in money form
						AcctNoBox.Text = dataIntermed.acctNo.ToString();
						PinBox.Text = dataIntermed.pin.ToString("D4");
						IndexBox.Text = dataIntermed.index.ToString();
						Bitmap bmp;
						using (var ms = new MemoryStream(dataIntermed.image))
						{
							bmp = new Bitmap(ms);
						}
						//Converting byte array to bitmap -> see below for reference
						Photo.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bmp.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
						ChangeUIElements(false);
						ProgressBar.Visibility = Visibility.Hidden;
					}
					else //If the response isn't successful
					{
						string errmsg = resp.Content;
						MessageBox.Show(errmsg);
						AfterError();
					}
					//Return ui back to normal
					ChangeUIElements(false);
					ProgressBar.Visibility = Visibility.Hidden;
					SearchBox.Visibility = Visibility.Visible;
				});
			});
		}


		/* Allows the user to search for a user at an index
		 * and handles if they search outside of that range
		 */
		private void SearchIndex(object sender, RoutedEventArgs e)
		{
			try
			{
				SearchBox.Text = "";
				int index = Int32.Parse(IndexBox.Text);
				Task search = SearchIndexAsync(sender, e, index);
				ProgressBar.Visibility = Visibility.Visible;

				ChangeUIElements(true);

				SearchBox.Visibility = Visibility.Hidden;
			}
			catch (FormatException ex) //catching index input as string
			{
				AfterError();
				MessageBox.Show("Input is in the incorrect format");
			}
		}

		/*Asynchronously searches for a user at that index*/
		private async Task SearchIndexAsync(object sender, RoutedEventArgs e, int index)
		{
			//Onclick get the index
			await Task.Run(() => //Async 
			{
				//Setting up the api method
				RestRequest request = new RestRequest("api/getvalues/" + index.ToString());
				IRestResponse resp = client.Get(request);//Uses Get over POST

				//JSON deserialiser to deserialize our object to the class we want

				//Async
				this.Dispatcher.Invoke(() =>
				{
					if (resp.IsSuccessful)//If the httpresponse was successful
					{
						TransitionModel transModel = JsonConvert.DeserializeObject<TransitionModel>(resp.Content);//Contains a message and the dataintermed obj
						DataIntermed dataIntermed = JsonConvert.DeserializeObject<DataIntermed>(transModel.message);//Getting the dataintermed obj 
							
						//Setting the values in the gui
						FNameBox.Text = dataIntermed.fName;
						LNameBox.Text = dataIntermed.lName;
						BalanceBox.Text = dataIntermed.bal.ToString("C");
						AcctNoBox.Text = dataIntermed.acctNo.ToString();
						PinBox.Text = dataIntermed.pin.ToString("D4");

						/* Sourced from to figure out how to change byte array to bitmap
						* https://stackoverflow.com/questions/21555394/how-to-create-bitmap-from-byte-array
						* 
						*/
						Bitmap bmp;
						using (var ms = new MemoryStream(dataIntermed.image))
						{
							bmp = new Bitmap(ms);
						}

						Photo.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bmp.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
					}
					else //if the httpresponse isn't successful
					{
						
						string errmsg = resp.Content;
						MessageBox.Show(errmsg);

						AfterError();//dispaly in everyfield that the user input is wrong
					}
					//Go back from searching visual
					ChangeUIElements(false);
					ProgressBar.Visibility = Visibility.Hidden;
					SearchBox.Visibility = Visibility.Visible;
				});

			});
		}

		/*If there is an error this is called that displays everything as errors*/
		private void AfterError()
		{
			FNameBox.Text = "Couldn't find that user";
			LNameBox.Text = "Couldn't find that user";
			BalanceBox.Text = "Couldn't find that user";
			AcctNoBox.Text = "Couldn't find that user";
			PinBox.Text = "Couldn't find that user";
			Photo.Source = null;
			ChangeUIElements(false);
			ProgressBar.Visibility = Visibility.Hidden;
			SearchBox.Visibility = Visibility.Visible;
		}


		/*Used to convert byte arrays to images*/
		public System.Drawing.Image byteArrayToImage(byte[] bytesArr)
		{
			using (MemoryStream mestr = new MemoryStream(bytesArr))
			{
				System.Drawing.Image img = System.Drawing.Image.FromStream(mestr);
				return img;
			}
		}

	}
}
