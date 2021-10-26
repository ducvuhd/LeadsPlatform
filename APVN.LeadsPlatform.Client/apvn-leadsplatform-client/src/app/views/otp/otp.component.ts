import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ResponseData, SystemCode } from 'src/app/models/ResponseData';
import { OtpPrivateKey, UrlOtp } from 'src/app/models/UrlOtp';
import { UserService } from 'src/app/services/user.service';
@Component({
  selector: 'app-otp',
  templateUrl: './otp.component.html',
  styleUrls: ['./otp.component.css']
})
export class OtpComponent implements OnInit {
  dataQRCode: OtpPrivateKey = new OtpPrivateKey();
  urlOtpModels: UrlOtp = new UrlOtp();
  constructor(
    private userService: UserService,
    private activeRouter: ActivatedRoute,
    private toaStr: ToastrService
  ) { 
    
  }

  ngOnInit(): void {
    this.activeRouter.params.subscribe(params => {
      const urlParams = params.param;
      if (urlParams !== undefined && urlParams !== null) {
        this.urlOtpModels.UrlOtp = urlParams;
        this.userService.getOTP(this.urlOtpModels).subscribe((res: any)=>{
          if (res.Code === SystemCode.Success) {
            this.dataQRCode = res.Data.OTPData as OtpPrivateKey;
          } else {
            this.toaStr.error(res.Message);
          }
        });
      }
    });
  }

}
