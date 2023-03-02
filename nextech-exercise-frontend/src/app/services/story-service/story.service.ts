import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import Story from 'src/app/models/story';

@Injectable({
  providedIn: 'root',
})
export class StoryService {
  private baseUrl: string = 'https://localhost:7111';

  constructor(private client: HttpClient) {}

  public getStories(pageSize: number, pageNumber: number): Array<Story> {
    let storiesFound: Story[] = [];

    this.client.get(`${this.baseUrl}/Stories?pageSize=${pageSize}&pageNumber=${pageNumber}`)
    .subscribe((response) =>{
      let auxStories = response as Story[];

      auxStories.forEach(val => storiesFound.push(val));
      
    });

    return storiesFound;
  }

  public getStoriesByName(filterName: string): Array<Story>{
    let storiesFound: Story[] = [];

    this.client.get(`${this.baseUrl}/Stories/GetByName?name=${filterName}`)
    .subscribe((response) =>{
      let auxStories = response as Story[];

      auxStories.forEach(val => storiesFound.push(val));
    })

    return storiesFound;
  }
}
