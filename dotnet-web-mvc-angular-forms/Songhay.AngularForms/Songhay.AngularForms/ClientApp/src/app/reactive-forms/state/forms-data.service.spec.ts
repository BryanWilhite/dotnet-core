import { HttpClientModule } from '@angular/common/http';
import { inject, TestBed, waitForAsync } from '@angular/core/testing';

import { FormsDataService } from './forms-data.service';

describe('FormsDataService', () => {
  let service: FormsDataService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule],
      providers: [
        {
          provide: 'BASE_URL_FOR_API',
          useFactory: () => 'http://localhost:3000/',
          deps: []
        },
        FormsDataService,
      ],
    });
    service = TestBed.inject(FormsDataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should see `json-server`',
    waitForAsync(
      inject(['BASE_URL_FOR_API', FormsDataService],
        (baseUri: string, formsDataService: FormsDataService) => {

          const uri = `${baseUri}posts/1`;
          formsDataService.client.get(uri).subscribe(data => {

            expect(data).not.toBeFalsy('The expected json-server data is not here.');

            const domainData = data as { id: number; title: string; author: string; }
            expect(domainData).not.toBeFalsy('The expected json-server domain data is not here.');

            expect(domainData.title).toEqual('json-server');

          });

        })
    )
  );

  it('should load formly data',
    waitForAsync(
      inject(['BASE_URL_FOR_API', FormsDataService],
        (baseUri: string, formsDataService: FormsDataService) => {

          const uri = `${baseUri}formly`;
          formsDataService.loadFormlyData().subscribe(data => {

            expect(data).not.toBeFalsy('The expected formly data is not here.');

            expect(formsDataService.formlyData).not.toBeFalsy('The expected formly domain data is not here.');

            const expectedProperties = ['form1', 'form2', 'form3'];

            expectedProperties.forEach(p => {
              expect(formsDataService.formlyData?.componentSet[p]).not.toBeFalsy(
                `The expected formly data property, \`${p}\`, is not here.`
              );
            });

          });

        })
    )
  );
});
