import { Observable, throwError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

export class BaseService 
{
    url: string = 'http://localhost:32805';
    
    public handleError(error: HttpErrorResponse): Observable<never>
    {
        let errorMessage = '';

        if (error.error instanceof ErrorEvent) 
        {
            errorMessage = error.error.message;
        } 
        else 
        {
            errorMessage = `Código do erro: ${error.status}, ` + `menssagem: ${error.message}`;
        }

        console.log(errorMessage);
        return throwError(errorMessage);
    }
}
