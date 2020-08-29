
#import <Foundation/Foundation.h>

@interface IosUIWidgets : NSObject
@end

@implementation IosUIWidgets
+(void)ShowDialogInfo: (NSString *) title message: (NSString*) msg okTitle:(NSString*) b1 {
    


    UIDatePicker *datePicker = [[UIDatePicker alloc] initWithFrame:CGRectZero];
    [datePicker setDatePickerMode:UIDatePickerModeDate];
    [datePicker addTarget:self action:@selector(onDatePickerValueChanged:) forControlEvents:UIControlEventValueChanged];

    UIAlertController *alertController = [UIAlertController alertControllerWithTitle:title message:msg preferredStyle:UIAlertControllerStyleAlert];
    
    UIAlertAction *okAction = [UIAlertAction actionWithTitle:b1 style:UIAlertActionStyleDefault handler:^(UIAlertAction * _Nonnull action) {
        [IOSNativePopUpsManager unregisterAllertView];
        UnitySendMessage("MobileDialogInfo", "OnOkCallBack",  [DataConvertor NSIntToChar:0]);
    }];
    [alertController addAction:okAction];
    
    
    [[[[UIApplication sharedApplication] keyWindow] rootViewController] presentViewController:alertController animated:YES completion:nil];
    _currentAllert = alertController;
}

@obj func dateChanged(datePicker : UIDatePicker) {
    let dateFormatter = DateFormatter()
    dateFormatter.dateFormat = "dd/MM/yyyy"

}

@end

extern "C"
{

    void OpenDateTimePicker(){

    }

}