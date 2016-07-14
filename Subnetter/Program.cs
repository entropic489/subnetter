using System;

namespace Subnetter
{
	class MainClass
	{
		public static int firstOctet;
		public static int secondOctet;
		public static int thirdOctet;
		public static int fourthOctet;

		public static int subnetNum;

		public static void Main (string[] args)
		{
			Console.WriteLine ("Give me your network ID.\n");
			Console.WriteLine ("What is your first octet?\n");
			firstOctet = Int32.Parse (Console.ReadLine ());
			Check (firstOctet);

			Console.WriteLine ("What is your second octet?\n");
			secondOctet = Int32.Parse (Console.ReadLine ());
			Check (secondOctet);

			Console.WriteLine ("What is your third octet?\n");
			thirdOctet = Int32.Parse (Console.ReadLine ());
			Check (thirdOctet);

			Console.WriteLine ("What is your fourth octet?\n");
			fourthOctet = Int32.Parse (Console.ReadLine ());
			Check (fourthOctet);

			Console.WriteLine ("How many subnets do you need?\n");
			subnetNum = Int32.Parse (Console.ReadLine ());

			Subnet[] subArr = new Subnet[subnetNum];

			for (int i = 0; i < subnetNum; i++) 
			{
				Console.WriteLine ("How many users for subnet " + (i + 1) + "?\n");
				subArr[i] = new Subnet();
				subArr [i].users = Int32.Parse (Console.ReadLine ());
				subArr [i].networkID = new int[4]{firstOctet, secondOctet, thirdOctet, fourthOctet};
				fourthOctet += MaskMaker (subArr [i].users);
				
				if(fourthOctet > 255)
				{
					fourthOctet -= 255;
					thirdOctet += 1;
				}
				if(thirdOctet > 255)
				{
					thirdOctet -= 255;
					secondOctet += 1;
				}
				if(secondOctet > 255)
				{
					secondOctet -= 255;
					firstOctet += 1;
				}
				if(firstOctet > 255)
				{
					Console.WriteLine ("Congratulations, you have exceeded the total number of IPv4 addresses in existence.");
					Environment.Exit (0);
				}

					subArr[i].broadcastID = new int[4] {
					firstOctet,
					secondOctet,
					thirdOctet,
					fourthOctet
				};

				Console.WriteLine ("Network ID for Subnet " + (i + 1) + "= " + subArr [i].networkID [0] + "." + subArr [i].networkID [1] + "." + subArr [i].networkID [2] + "." + subArr [i].networkID [3]);
				Console.WriteLine ("Broadcast ID for Subnet " + (i + 1) + "= " + subArr [i].broadcastID [0] + "." + subArr [i].broadcastID [1] + "." + subArr [i].broadcastID [2] + "." + subArr [i].broadcastID [3]);

				fourthOctet += 1;
			}

			Console.ReadKey ();

		}

		public static void Check(int x)
		{
			if (x > 255) {
				Console.WriteLine ("Invalid\n");
				Environment.Exit (0);
			} else
				return;
		}

		public struct Subnet
		{
			public int[] broadcastID;
			public int[] networkID;
			public int[] subnetMask;
			public int users;
		}

		public static int MaskMaker(int users)
		{
			int broadcast;

			if (users == 1) {
				broadcast = 1;
			} else if (users > 1 && users < 3) {
				broadcast = 2;
			} else if (users > 2 && users < 4) {
				broadcast = 4;
			} else if (users >= 4 && users < 8) {
				broadcast = 8;
			} else if (users >= 8 && users < 16) {
				broadcast = 16;
			} else if (users >= 16 && users < 32) {
				broadcast = 32;
			} else if (users >= 32 && users < 64) {
				broadcast = 64;
			} else if (users >= 64 && users < 128) {
				broadcast = 128;
			} else if (users >= 128 && users < 255) {
				broadcast = 255;
			} else {
				return 0;
			}
				
			return broadcast;
		}
	}
}
