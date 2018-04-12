import { NotFoundError } from "./not-found-error";
import { BadRequestError } from "./bad-request-error";
import { AppError } from "./app-error";
import { Observable } from "rxjs/Observable";

export function handleError(error: Response) {
    if (error.status === 404)
        return Observable.throw(new NotFoundError(error))
    if (error.status === 400)
        return Observable.throw(new BadRequestError(error));

    return Observable.throw(new AppError(error))
}