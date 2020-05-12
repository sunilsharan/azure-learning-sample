import { Component, OnInit } from "@angular/core";
import { IUser } from '../service/user';
import { UserService } from '../service/user.service';
import { Router } from '@angular/router';
import { MessageService } from '../service/message.service';

@Component ({
     templateUrl:"./user-list.component.html"
})
export class UserListComponent implements OnInit
{
    pageTitle: string = 'Registered Users'
    errorMessage: string;
    users: IUser[]=[];

    
    constructor (private userService : UserService, private router:Router, private message: MessageService) {
        
    }
    onEdit(user:IUser)
    {
        
        //this.router.navigate(['/Register'],{ queryParams: {id: user.id} ,{pk:user.address.postalCode }});
       // console.log(user.address)
        //console.log(user.address.postalcode)
        this.router.navigate(['/Register',{id:user.id ,pk:user.address.postalcode }])
    }

    onButtonClick() {
        this.router.navigate(['/Register'] );
      }

      onDeleteUser(user: IUser)
      {
        this.userService.deleteUser(user);
        this.loadUsers();
      }

    ngOnInit(): void {
        this.loadUsers();
    }

    loadUsers()
    {
        this.userService.getUsers().subscribe(
            {
                next:user => 
               {
                this.users=user
               },
               error: err => this.errorMessage = err
           }
        );
    }
}
