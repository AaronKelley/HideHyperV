# HideHyperV
A program that hides the Hyper-V network adapter from certain views in Windows Explorer and Network and Sharing Center.

When Hyper-V is enabled, a network adapter is created in Windows that is used to allow for communication between the VMs, the host, and the host's network environment.  This is necessary and useful, but the Hyper-V network adapter shows up as an "unknown network" in some views around Windows:

![System tray](https://jpjvmg.bn.files.1drv.com/y4mVb_Xmbea4oTfnKU1sjXX-4B9YCacUFTlJYlaKtYn5oY4_88yK7ingwu1tyi30QK8Op0Ycxmz6UYFaArU_re7ikzAWXB-VCSXA5MAhAQU81luVw7aUNFFGw8KjvyewmAPhrnTVupJhY31TviBomdfu0co7pdzuE8pRENYN7GQZzpvlo1ZkbwpQWj3EwoJ1tYd4W9zEe_j0cvchqZ_jvpqMQ?width=314&height=140&cropmode=none) (System tray)

![Network and Sharing Center](https://hasqbq.bn.files.1drv.com/y4mzUMYqo30S5MJc5M63jAm6VwtppXE-TLZR9VgLvesfEOFDEswzXiNz6otwkL24HiLmWO0Ezf-8nXKUdNHXPvOA0Y6o5yD_TNXCLPSCU-J0DLD1MN9kV83GUxX8IHdUpdTGW_90HJJkGJhXQow10kRiCT67TSYq_4NA94uHr5LPJAePCQD9E9ZTGeRxuhNnNFIVOM9ltuytWy3D8fsbeo64Q?width=548&height=63&cropmode=none) (Network and Sharing Center)

VMware Workstation creates similar network adapters but properly hides them so that they do not show up in these views.  I looked into this and found the proper registry value to apply to hide the Hyper-V Ethernet adapter, so that it does not show up as an "unknown network".  Unfortunately, this value does not "stick" and needs to be reapplied at boot time.  Also, the order of network adapters in the registry can change to just creating and applying a REG file does not work.  Hence, this program.

The program dynamically locates the registry key for the Hyper-V Ethernet adapter and then applies the registry value to hide it.  This program should be scheduled to run at boot time with Task Scheduler.

(Note, I am still testing its effectiveness from an automation perspective.  It might also be necessary to disable and re-enable the Hyper-V network adapter to get the change to "stick".  If this is needed then I will add the necessary logic to the program.)
