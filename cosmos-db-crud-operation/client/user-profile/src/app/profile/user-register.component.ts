import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { IUser } from '../service/user';
import { Router, ActivatedRoute } from '@angular/router';
import { UserService } from '../service/user.service';
import { MessageService } from '../service/message.service';
import { async } from '@angular/core/testing';
import { Subscription } from 'rxjs';
import { DatePipe } from '@angular/common';

@Component({
    templateUrl: "./user-register.component.html"
})

export class UserRegister implements OnInit {
    //userForm: FormGroup;
    id: string;
    partitionKey: string
    userForm = this.fb.group({
        id: new FormControl(''),
        firstName: new FormControl('', [Validators.required, Validators.minLength(2)]), //Require and must be 2 character long
        lastName: new FormControl(''),
        email: new FormControl(''),
        dob: new FormControl(''),
        address: this.fb.group(
            {
                addressLine1: new FormControl(''),
                addressLine2: new FormControl(''),
                state: new FormControl(''),
                city: new FormControl(''),
                postalcode: new FormControl('', [Validators.required, Validators.minLength(5)])
            })
    });
    ngOnInit(): void {
       
        if ( this.id != undefined) {
            this.service.getUser(this.id,this.partitionKey).subscribe(
                data=>
                {
                    console.log(data);
                    this.userForm.patchValue({
                        firstName: data.firstName,
                        lastName: data.lastName,
                        email:data.email,
                        dob: new DatePipe('en-US').transform(data.dob, 'yyyy-MM-dd'),
                        address: {
                          addressLine1: data.address.addressLine1,
                          addressLine2:data.address.addressLine2,
                          city:data.address.city,
                          state:data.address.state,
                          postalcode:data.address.postalcode
                        }
                      });
                  
                }
            )
        }

    }

    constructor(public fb: FormBuilder,
        private router: Router,
        private route: ActivatedRoute,
        private service: UserService,
        private datePipe:DatePipe) {

            this.id=  this.route.snapshot.paramMap.get("id")
            this.partitionKey= this.route.snapshot.paramMap.get("pk")
            if(this.id !=undefined)
            {
                this.pageTitle="Update Profile";
                this.buttonText="Update";
            }
    }

    pageTitle: string = "Register Profile";
    buttonText:string="Save";
    submitForm() {
        this.service.createUser(this.userForm.value);
        console.log(this.userForm.value);
        this.router.navigate(['/users'])
    }
} 