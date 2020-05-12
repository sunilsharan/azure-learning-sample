import {Injectable} from '@angular/core'
import { HttpClient, HttpErrorResponse } from '@angular/common/http';

import{IUser} from './user'
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';


@Injectable({
    providedIn: 'root'
  })

export class UserService
{
    private serviceUrl ="http://localhost:60075";

    getConfig<T>()
    {
      return this.http.get(this.serviceUrl)
    }
    constructor (private http:HttpClient)
    {

    }
    getUsers(): Observable<IUser[]> {
      return this.http.get<IUser[]>(this.serviceUrl + '/api/userprofile')
      .pipe(catchError(this.handleError))

      /* Console data log with error handler
       .pipe(
        tap(data => console.log('All: ' + JSON.stringify(data))),
        catchError(this.handleError)
      */
  }

  getUser(userid:string, partitionKey: string): Observable<IUser> {
    return this.http.get<IUser>(this.serviceUrl + '/api/userprofile/'+userid+"?partitionKey="+partitionKey)
    .pipe(catchError(this.handleError))

    /* Console data log with error handler
     .pipe(
      tap(data => console.log('All: ' + JSON.stringify(data))),
      catchError(this.handleError)
    */
}


  createUser(user:IUser){
      this.http.post(this.serviceUrl +"/api/userprofile",user).subscribe(
        response=> console.log(response),
        error=> console.log(error)
      )
    }

    deleteUser(user:IUser){
      this.http.delete(this.serviceUrl +"/api/userprofile/"+user.id).subscribe(
        response=> console.log(response),
        error=> console.log(error)
      )
    }

    private handleError(err: HttpErrorResponse) 
    {
        let errorMessage = '';
        if (err.error instanceof ErrorEvent) {
        // A client-side or network error occurred. Handle it accordingly.
        
        errorMessage = `An error occurred: ${err.error.message}`;
        } else {
          errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`;
        }
        console.error(errorMessage);
        return throwError(errorMessage);
    }
}