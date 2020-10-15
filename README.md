# TheProject
let private drivers offer rides and drive together, this modern way is flexible and energy/money saving. It may help solving congestion problems since more people will go in one car, which means less cars on the road.<br>
# TODO
waiting backendless or admob team:<br>
device registration, confirmation email, finder location response language,push notifications, alertBusiness(need pus notification only), logout by push notification, confirm email, 
use system language to set language,chatting system, native ads<br>

fix backendless code(decrease requests),input field when keyboard open issue,show recent locations in findlocationpanel and fix item design, add ride map pick adn its items design, all items should be inherited form item class add select usnelect clear moveup move down..,
 GUI, notification panel, searchpanel automatic end date +24, if user change end date this will be disabled, how it works, terms & conditions, privacy policy, licenses, user in cache(person, user already done, need to think what else should we add it in cache),
backendless validation, backup data, input field error when pressing back android, contact us( send email for user to confirm that we receive his email)<br>

loubani arabic language(search about change language,input fields,xml files)<br>
adel ads mediation<br>

# ERRORS:
All request should check on userobject id and something else<br>
UserBusiness <br>
	deleteUser:<br>
		we should store in archive and not delete them(high)<br>
	getLoggedInUser:<br>
		we should get user by id and somthing else(high security)<br>
Review:
generally comment is optional.
if rate is less than 3 stars, user should give a reason from a dropdown, if the reason doesnt exist there, he can choose "something else" and the comment 
is mandatory then. if he give the reason, no need for comment (optional)
if the rate is more than 3 stars, user can give reason what he liked the most about the trip.

EDITRIDE:<br>
	if there's passengers:<br>
	stopTime: can only be less or equal to the original //do it in backendless:<br>
	price: can only be less  //do it in backednless:<br>
	comment: can't be changed, but you can add more information //do it in backendless:<br>
	kidSeat: can be changed to TRUE, but can't be changed to FALSE //do it in backendless:<br>:<br>

CANCELRIDE::<br>
	case2: if reservation not null and before 48h of leaving time->delete and notify passengers  BACKENDLESS NOTIFICATION:<br>
	case3: if reservation not null and after 48h or less of leaving time -> BACKENDLESS NOTIFICATION:<br>:<br>
	
Cancel Reserve Seat::<br>
	case2: before 48h of leaving time->delete and notify driver:<br>
	case3: after 48h or less of leaving time -> delete reserve with reason of deletion and notify driver and let him rate passenger.:<br>:<br>

#TODO::<br>
Notes::<br>
Gps to get location and set region depend on it.:<br>:<br>
 
# TASKS
Ads System.
Ratings System.
My Rides.
All edits.(Profile, MyRides, Cancel Reservation, Remove Reservation) changePassword forgetpassword, Update Regions)
Alert System.
Splash Screen.
Remove unity splash screen.
Arabic/Lanaguage.
Feedback/Bugs Report.
Contact System.
Implement Validation.

# Unity Version
<h4>Version 2019.3.2f1 Personal</h4>

Prefabs:<br>
AddRidePanel <br>
AddCarPanel <br>
AlertPanel<br>
BioPanel<br>
ChattinessPanel<br>
RatePanel<br>
UserDetailsPanel<br>
UserRatings <br>
FindLocationPanel <br>
NotificationsPanel <br>
HowItWorksPanel <br>
ContactUsPanel<br>
YourRidesPanel<br>
ReportUserPanel<br>
DirectionsFinderPanel<br>
SchedulePanel <br>
PrivacyPolicyPanel<br>
LicensesPanel<br>
TermsConditionsPanel<br>
become a driver<br>
ProfilePanel<br>
SettingsPanel  //language<br>
BookingHistoryPanel<br>
SearchPanel <br>
ChatPanel <br>
Alert details<br>
InboxPanel<br>
LoginPanel<br>
RegisterPanel<br>
AccountPanel<br>
Phone<br>
ChangePasswordPanel<br>
Tasks: <br>
Make first english language xml file and test it in any free prefab<br>
Test app on android devices(different screen sizes)<br>
Test app on ios devices(different screen sizes)<br>
Test app on tablet
Test the one picker for date and time (look for it in schedulepanel or searchpanel or addRidePanel)
Track Bugs and report them in issues section.

<h3>Beta ER Model</h3>
<img src="https://github.com/Hasankanso/TheProject/blob/master/DBimage.png"\>

<h3>Beta UML diagramm</h3>
<img src="https://github.com/Hasankanso/TheProject/blob/master/UML.png"\>


<h3>Communicator UML diagramm</h3>
<img src="https://github.com/Hasankanso/TheProject/blob/master/Requests.png"\>
